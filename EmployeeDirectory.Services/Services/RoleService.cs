using EmployeeDirectory.Data.Data.Services;
using EmployeeDirectory.DATA;
using EmployeeDirectory.Interfaces;
using EmployeeDirectory.Models;
using EmployeeDirectory.Models.Models;
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
        public int Add(Role role)
        {
            List<Role> roles = GetAllRoles();


            string departmentId = roleDataService.GetDepartmentId(role.Department);
            string locationId = roleDataService.GetLocationId(role.Location);
            

            int rowsAffected = roleDataService.Add(role, departmentId, locationId);
            return rowsAffected;
        }

        public bool DoesRoleExists(string roleName, string location)
        {
            List<Role> roles = GetAllRoles();
            return roles.Any(role => role.Name.Equals(roleName) && role.Location.Equals(location));
        }
        public string GenerateRoleId()
        {
            List<Role> roles = GetAllRoles();
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
                throw new ArgumentException("Invalid role ID format.");
            }
            return newRoleId;
        }

        public List<Role> GetAllRoles()
        {
            List<Role> roles = roleDataService.GetRoles();
            return roles;
        }

        public Role? GetRoleById(string id)
        {

            Role? role = roleDataService.GetRoleById(id);
            if (role == null)
            {
                return null;
            }
            return role;
        }
        
        public List<Tuple<string, string, string>> GetRoleNames()
        {
            List<Role> roles = GetAllRoles();
            List<Tuple<string, string, string>> roleDetails = roles.Select(role => new { role.Id, role.Name, role.Location})
                                                    .AsEnumerable()
                                                    .Select(role => new Tuple<string, string, string>(role.Id, role.Name, role.Location)).ToList();

            return roleDetails; 
        }

        public List<string> GetAllDepartments()
        {
            List<Role> roles = GetAllRoles();
            return roles.Select(role => role.Department).Distinct().ToList()!;
        }


        public bool DoesLocationExist(string location)
        {
            List<Role> roles = GetAllRoles();
            return roles.Any(role => role.Location == location);
        }

        public List<string> GetAllRoleNamesByDepartment(string department)
        {
            List<Role> roles = GetAllRoles();
            return roles.Where(role => role.Department == department).Select(role => role.Name).Distinct().ToList()!;
        }

        public List<string> GetAllLocationByDepartmentAndRoleNames(string roleName)
        {
            List<Role> roles = GetAllRoles();
            return roles.Where(role => role.Name == roleName).Select(role => role.Location).Distinct().ToList()!;
        }
    }
}