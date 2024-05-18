using EmployeeDirectory.Models;

namespace EmployeeDirectory.Data.Data.Services
{
    public interface IRoleDataService
    {
        List<Role> GetRoles();
        Role GetRoleById(string roleId);
        public string GetLocationId(string locationName);
        public string GetDepartmentId(string departmentName);
    }
}