using EmployeeDirectory.Interfaces;
using EmployeeDirectory.Models;
using EmployeeDirectory.Services;

namespace EmployeeDirectory.Controllers
{
    public class RoleController : IRoleController
    {
        private IRoleService roleService;

        public RoleController(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        public List<Role> ViewRoles()
        {
            return roleService.GetAllRoles().DataList;
        }
        public int Add(Role role)
        {
            return roleService.Add(role).Data;
        }
        public bool DoesRoleExists(string roleName, string location)
        {
            return roleService.DoesRoleExists(roleName, location).Data;
        }
        public string GenerateRoleId()
        {
            return roleService.GenerateRoleId().Data;
        }

        public List<Tuple<string, string, string>> GetRoleNames()
        {   
            
            return roleService.GetRoleNames().Data;
        }

        public List<string> GetAllDepartments()
        {
            return roleService.GetAllDepartments().Data;
        }

        public List<string> GetAllRoleNamesByDepartment(string department)
        {

            return roleService.GetAllRoleNamesByDepartment(department).Data;
        }

        public List<string> GetAllLocationByDepartmentAndRoleNames(string roleName)
        {

            return roleService.GetAllLocationByDepartmentAndRoleNames(roleName).Data;
        }
    }
}
