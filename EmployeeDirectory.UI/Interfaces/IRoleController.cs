using EmployeeDirectory.Models;

namespace EmployeeDirectory.Interfaces
{
    public interface IRoleController
    {
        List<Role> ViewRoles();
        List<string> GetAllDepartments();
        string GenerateRoleId();
        int Add(Role role);
        bool DoesRoleExists(string roleName, string location);
        List<string> GetAllLocationByDepartmentAndRoleNames(string roleName);
        List<string> GetAllRoleNamesByDepartment(string department);
        List<Tuple<string,string, string>> GetRoleNames();
    }
}