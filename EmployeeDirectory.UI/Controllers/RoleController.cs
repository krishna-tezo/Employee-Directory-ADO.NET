using EmployeeDirectory.Interfaces;
using EmployeeDirectory.Models;

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
            return roleService.GetAllRoles();
        }
        public Role Add(Role role)
        {
            return roleService.AddRole(role);
        }

        public string GetRoleId(string roleName, string location)
        {
            return roleService.GenerateRoleId(roleName, location);
        }
        public List<Tuple<string, string>> GetRoleNames()
        {
            return roleService.GetRoleNames();
        }

        public List<string> GetAllDepartments()
        {
            return roleService.GetAllDepartments();
        }

        public List<string> GetAllRoleNamesByDepartment(string department)
        {

            return roleService.GetAllRoleNamesByDepartment(department);
        }

        public List<string> GetAllLocationByDepartmentAndRoleNames(string roleName)
        {

            return roleService.GetAllLocationByDepartmentAndRoleNames(roleName);
        }
    }
}
