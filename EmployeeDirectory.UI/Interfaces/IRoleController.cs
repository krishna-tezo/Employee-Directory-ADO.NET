using EmployeeDirectory.Models;

namespace EmployeeDirectory.Controllers
{
    public interface IRoleController
    {
        ServiceResult<int> Add(Role role);
        ServiceResult<bool> DoesRoleExists(string roleName, string location);
        ServiceResult<string> GenerateRoleId();
        ServiceResult<List<string>> GetAllDepartments();
        ServiceResult<List<string>> GetAllLocationByDepartmentAndRoleNames(string roleName);
        ServiceResult<List<string>> GetAllRoleNamesByDepartment(string department);
        ServiceResult<List<Tuple<string, string, string>>> GetRoleNames();
        ServiceResult<Role> ViewRoles();
    }
}