using EmployeeDirectory.Models.Models;

namespace EmployeeDirectory.UI.Interfaces
{
    public interface IProjectController
    {
        List<Tuple<string, string, string>> GetProjectNames();
        List<Project> ViewProjects();
    }
}