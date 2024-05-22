using EmployeeDirectory.Models;
using EmployeeDirectory.Models.Models;

namespace EmployeeDirectory.Services.Services
{
    public interface IProjectService
    {
        public ServiceResult<Project> GetProjects();
        public ServiceResult<Project> GetProjectById(string id);
        public ServiceResult<List<Tuple<string, string, string>>> GetProjectNames();
    }
}