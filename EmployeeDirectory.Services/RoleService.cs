using EmployeeDirectory.Data.Data.Services;
using EmployeeDirectory.Models;
using System.Data;

namespace EmployeeDirectory.Services
{
    public class RoleService : IRoleService
    {

        private IRoleDataService roleDataService;

        public RoleService(IRoleDataService roleDataService)
        {
            this.roleDataService = roleDataService;
        }

        public ServiceResult<Role> GetAllRoles()
        {
            try
            {
                List<Role> roles = roleDataService.GetRoles();
                return ServiceResult<Role>.Success(roles);
            }
            catch (Exception ex)
            {
                return ServiceResult<Role>.Fail(ex.Message);
            }
        }


        public ServiceResult<Role> GetRoleById(string id)
        {
            try
            {
                Role role = roleDataService.GetRoleById(id);
                if (role == null)
                {
                    return ServiceResult<Role>.Fail("Role doesn't exist");
                }
                return ServiceResult<Role>.Success(role);
            }
            catch (Exception ex)
            {
                return ServiceResult<Role>.Fail(ex.Message);
            }
        }

        public ServiceResult<int> Add(Role role)
        {
            try
            {
                List<Role> roles = GetAllRoles().DataList;

                string departmentId = roleDataService.GetDepartmentId(role.Department);
                string locationId = roleDataService.GetLocationId(role.Location);

                int rowsAffected = roleDataService.Add(role, departmentId, locationId);
                return ServiceResult<int>.Success(rowsAffected, $"{rowsAffected} data has been inserted");
            }
            catch (Exception ex)
            {
                return ServiceResult<int>.Fail(ex.Message);
            }
        }

        public ServiceResult<bool> DoesRoleExists(string roleName, string location)
        {
            try
            {
                List<Role> roles = GetAllRoles().DataList;
                bool exists = roles.Any(role => role.Name.Equals(roleName) && role.Location.Equals(location));
                return ServiceResult<bool>.Success(exists);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.Fail(ex.Message);
            }
        }
        public ServiceResult<string> GenerateRoleId()
        {
            try
            {
                List<Role> roles = GetAllRoles().DataList;
                string lastRoleId = roles.Last().Id;

                string newRoleId;
                string prefix = "RL";
                string numericPart = lastRoleId.Substring(prefix.Length);

                if (int.TryParse(numericPart, out int numericId))
                {
                    int newNumericId = numericId + 1;
                    newRoleId = prefix + newNumericId.ToString("D4");
                }
                else
                {
                    return ServiceResult<string>.Fail("Invalid role ID format.");
                }
                return ServiceResult<string>.Success(newRoleId);
            }
            catch (Exception ex)
            {
                return ServiceResult<string>.Fail(ex.Message);
            }
        }


        public ServiceResult<List<Tuple<string, string, string>>> GetRoleNames()
        {
            try
            {
                List<Role> roles = GetAllRoles().DataList;

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
                List<Role> roles = GetAllRoles().DataList;
                List<string> departments = roles.Select(role => role.Department).Distinct().ToList();
                return ServiceResult<List<string>>.Success(departments);
            }
            catch (Exception ex)
            {
                return ServiceResult<List<string>>.Fail(ex.Message);
            }
        }


        public ServiceResult<bool> DoesLocationExist(string location)
        {
            try
            {
                List<Role> roles = GetAllRoles().DataList;
                bool exists = roles.Any(role => role.Location == location);
                return ServiceResult<bool>.Success(exists);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.Fail(ex.Message);
            }
        }

        public ServiceResult<List<string>> GetAllRoleNamesByDepartment(string department)
        {
            try
            {
                List<Role> roles = GetAllRoles().DataList;
                List<string> roleNames = roles.Where(role => role.Department == department).Select(role => role.Name).Distinct().ToList();
                return ServiceResult<List<string>>.Success(roleNames);
            }
            catch (Exception ex)
            {
                return ServiceResult<List<string>>.Fail(ex.Message);
            }
        }

        public ServiceResult<List<string>> GetAllLocationByDepartmentAndRoleNames(string roleName)
        {
            try
            {
                List<Role> roles = GetAllRoles().DataList;
                List<string> locations = roles.Where(role => role.Name == roleName).Select(role => role.Location).Distinct().ToList();
                return ServiceResult<List<string>>.Success(locations);
            }
            catch (Exception ex)
            {
                return ServiceResult<List<string>>.Fail(ex.Message);
            }
        }
    }
}

