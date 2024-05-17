using EmployeeDirectory.Models.Models;

namespace EmployeeDirectory.Services.Services
{
    public interface IProjectService
    {
        List<Project> GetProjects();
        Project GetProjectById(string id);
        List<Tuple<string, string,string>> GetProjectNames();
    }
}