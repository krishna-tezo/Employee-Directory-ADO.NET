using EmployeeDirectory.Models;

namespace EmployeeDirectory.Interfaces
{
    public interface IRoleService
    {
        Role AddRole(Role role);
        string GenerateRoleId(string roleName, string location);
        Role GetRoleById(string id);
        List<string> GetAllDepartments();
        List<string> GetAllLocationByDepartmentAndRoleNames(string roleName);
        List<string> GetAllRoleNamesByDepartment(string department);
        List<Role> GetAllRoles();

    }
}