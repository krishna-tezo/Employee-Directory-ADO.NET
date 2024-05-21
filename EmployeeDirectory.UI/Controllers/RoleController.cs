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
        public int Add(Role role)
        {
            return roleService.Add(role);
        }
        public bool DoesRoleExists(string roleName, string location)
        {
            return roleService.DoesRoleExists(roleName, location);
        }
        public string GenerateRoleId()
        {
            return roleService.GenerateRoleId();
        }

        public List<Tuple<string, string, string>> GetRoleNames()
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
