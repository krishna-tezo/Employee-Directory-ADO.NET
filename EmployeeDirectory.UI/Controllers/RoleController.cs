using EmployeeDirectory.Models;
using EmployeeDirectory.Models.Models;
using EmployeeDirectory.Services;
using EmployeeDirectory.UI.ViewModels;
using System.Data;

namespace EmployeeDirectory.Controllers
{
    public class RoleController : IRoleController
    {
        private readonly ICommonServices commonServices;

        public RoleController(ICommonServices commonServices)
        {
            this.commonServices = commonServices;
        }

        //View All Roles
        public ServiceResult<RoleView> ViewRoles()
        {

            List<Role> roles = commonServices.GetAll<Role>().DataList;
            List<Department> departments = commonServices.GetAll<Department>().DataList;
            List<Location> locations = commonServices.GetAll<Location>().DataList;

            var rolesToView = roles
                .Join(departments, role => role.DepartmentId, department => department.Id, (role, department) => new { Role = role, Department = department })
                .Join(locations, rd => rd.Role.LocationId, location => location.Id, (rd, location) => new RoleView
                {
                    Id = rd.Role.Id,
                    Name = rd.Role.Name,
                    Department = rd.Department.Name,
                    Description = rd.Role.Description,
                    Location = location.Name
                })
                .ToList();

            if (roles != null)
            {
                return ServiceResult<RoleView>.Success(rolesToView);
            }
            else
            {
                return ServiceResult<RoleView>.Fail("Some Error Occurred");
            }
        }

        //Add a role
        public ServiceResult<int> Add(RoleView viewRole)
        {
            Role role = new Role();
            
            string departmentId = commonServices.GetIdFromName<Department>(viewRole.Department).Data;
            if (departmentId == null)
            {
                departmentId = commonServices.GenerateNewId<Department>().Data;
            }

            string locationId = commonServices.GetIdFromName<Location>(viewRole.Location).Data;
            if (locationId == null)
            {
                
                locationId = commonServices.GenerateNewId<Location>().Data;
                Location location = new Location
                {
                    Id = locationId,
                    Name = viewRole.Location
                };
                commonServices.Add(location);
            }
            role.Id = viewRole.Id;
            role.Name = viewRole.Name;
            role.Description = viewRole.Description;
            role.LocationId = locationId;
            role.DepartmentId = departmentId;

            return commonServices.Add(role);
        }

        //Does a role exist with the same name and location
        public ServiceResult<bool> DoesRoleExists(string roleName, string locationName)
        {
            try
            {
                List<Role> roles = commonServices.GetAll<Role>().DataList;
                List<Location> locations = commonServices.GetAll<Location>().DataList;

                var result = roles
                    .Join(locations, role => role.LocationId, location => location.Id, (role, location) => new { Role = role, Location = location })
                    .Any(rl => rl.Role.Name == roleName && rl.Location.Name == locationName);

                return ServiceResult<bool>.Success(result);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.Fail("Database Issue: " + ex.Message);
            }
        }

        //Generate a new Role Id
        public ServiceResult<string> GenerateRoleId()
        {
            return commonServices.GenerateNewId<Role>();
        }

        // Get All Role Names along with location name
        public ServiceResult<List<Tuple<string, string, string>>> GetRoleNamesWithLocation()
        {
            // Return RoleId, RoleName and Location
            try
            {
                List<RoleView> roles = ViewRoles().DataList;
                List<Tuple<string, string, string>> roleDetails = roles
                    .Select(role => new { role.Id, role.Name, role.Location })
                    .OrderBy(role => role.Name)
                    .Select(role => new Tuple<string, string, string>(role.Id, role.Name, role.Location))
                    .ToList();

                return ServiceResult<List<Tuple<string, string, string>>>.Success(roleDetails);
            }
            catch (Exception ex)
            {
                return ServiceResult<List<Tuple<string, string, string>>>.Fail(ex.Message);
            }
        }

        
        public ServiceResult<List<string>> GetAllDepartments()
        {
            try
            {
                List<Department> departments = commonServices.GetAll<Department>().DataList;
                List<string> departmentsName = departments.Select(dept => dept.Name).Distinct().ToList();
                return ServiceResult<List<string>>.Success(departmentsName);
            }
            catch (Exception ex)
            {
                return ServiceResult<List<string>>.Fail(ex.Message);
            }
        }
    }
}
