using EmployeeDirectory.Models.Models;

namespace EmployeeDirectory.Data.Data.Services
{
    public interface IProjectDataService
    {
        List<Project> GetProjects();
        Project GetProjectById(string id);
    }
}