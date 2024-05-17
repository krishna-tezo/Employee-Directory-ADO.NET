using EmployeeDirectory.Models;

namespace EmployeeDirectory.Interfaces
{
    public interface IRoleController
    {
        Role Add(Role role);
        List<string> GetAllDepartments();
        List<string> GetAllLocationByDepartmentAndRoleNames(string roleName);
        List<string> GetAllRoleNamesByDepartment(string department);
        List<Tuple<string,string>> GetRoleNames();
        string GetRoleId(string roleName, string location);
        List<Role> ViewRoles();
    }
}