using EmployeeDirectory.Models;

namespace EmployeeDirectory.Services
{
    public interface IRoleService
    {
        ServiceResult<int> Add(Role role);
        ServiceResult<bool> DoesLocationExist(string location);
        ServiceResult<bool> DoesRoleExists(string roleName, string location);
        ServiceResult<string> GenerateRoleId();
        ServiceResult<List<string>> GetAllDepartments();
        ServiceResult<List<string>> GetAllLocationByDepartmentAndRoleNames(string roleName);
        ServiceResult<List<string>> GetAllRoleNamesByDepartment(string department);
        ServiceResult<Role> GetAllRoles();
        ServiceResult<Role> GetRoleById(string id);
        ServiceResult<List<Tuple<string, string, string>>> GetRoleNames();
    }
}