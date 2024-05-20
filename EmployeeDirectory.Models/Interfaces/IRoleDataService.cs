using EmployeeDirectory.Models;

namespace EmployeeDirectory.Data.Data.Services
{
    public interface IRoleDataService
    {
        List<Role> GetRoles();
        Role GetRoleById(string roleId);
        int Add(Role role, string locationId, string departmentId);
        public string GetLocationId(string locationName);
        public string GetDepartmentId(string departmentName);
    }
}