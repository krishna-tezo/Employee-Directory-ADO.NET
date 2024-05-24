using EmployeeDirectory.Models;
using EmployeeDirectory.Models.SummaryModels;

namespace EmployeeDirectory.Data.Data.Services
{
    public interface IRoleDataService
    {
        public RoleSummary GetRolesSummaryById(string id);
        public List<RoleSummary> GetRolesSummary();
    }
}