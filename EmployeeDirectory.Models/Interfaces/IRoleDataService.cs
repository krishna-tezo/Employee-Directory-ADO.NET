using EmployeeDirectory.Models;

namespace EmployeeDirectory.Data.Data.Services
{
    public interface IRoleDataService
    {
        List<Role> GetRoles();
        Role GetRoleById(string roleId);
    }
}