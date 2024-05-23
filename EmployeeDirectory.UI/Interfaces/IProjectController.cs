using EmployeeDirectory.Models;
using EmployeeDirectory.Models.Models;

namespace EmployeeDirectory.UI.Interfaces
{
    public interface IProjectController
    {
        ServiceResult<List<Tuple<string, string, string>>> GetProjectNames();
        ServiceResult<Project> ViewProjects();
    }
}