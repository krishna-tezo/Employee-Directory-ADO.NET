using EmployeeDirectory.Models;
using EmployeeDirectory.UI.ViewModels;

namespace EmployeeDirectory.Controllers
{
    public interface IRoleController
    {
        ServiceResult<int> Add(RoleView role);
        ServiceResult<bool> DoesRoleExists(string roleName, string locationName);
        ServiceResult<string> GenerateRoleId();
        ServiceResult<List<string>> GetAllDepartments();
        public ServiceResult<List<Tuple<string, string, string>>> GetRoleNamesWithLocation();
        ServiceResult<RoleView> ViewRoles();
    }
}