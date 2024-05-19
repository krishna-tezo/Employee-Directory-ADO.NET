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

        private IJsonDataHandler jsonDataHandler;
        private IRoleDataService roleDataService;
       
        public RoleService(IJsonDataHandler jsonDataHandler, IRoleDataService roleDataService)
        {
            this.jsonDataHandler = jsonDataHandler;
            this.roleDataService = roleDataService;
        }
        public Role AddRole(Role role)
        {
            List<Role> roles = GetAllRoles();
            
            string departmentId = roleDataService.GetDepartmentId(role.Department);
            string locationId = roleDataService.GetLocationId(role.Location);
            roleDataService.Add(role, departmentId, locationId);
            return role;
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

        public string GenerateRoleId(string roleName, string location)
        {
            return roleName[..3] + location[..3];
        }
        
        public List<Tuple<string, string>> GetRoleNames()
        {
            List<Role> roles = GetAllRoles();
            List<Tuple<string, string>> roleDetails = roles.Select(role => new { role.Id, role.Name})
                                                    .AsEnumerable()
                                                    .Select(role => new Tuple<string, string>(role.Id, role.Name)).ToList();

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