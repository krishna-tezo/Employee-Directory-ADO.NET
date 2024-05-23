using EmployeeDirectory.Models;
using EmployeeDirectory.Models.Models;
using EmployeeDirectory.Services.Services;
using EmployeeDirectory.UI.Interfaces;


namespace EmployeeDirectory.UI.Controllers
{
    public class ProjectController : IProjectController
    {
        private readonly IProjectService projectService;

        public ProjectController(IProjectService projectService)
        {
            this.projectService = projectService;
        }

        public ServiceResult<Project> ViewProjects()
        {
            return projectService.GetProjects();
        }

        public ServiceResult<List<Tuple<string, string, string>>> GetProjectNames()
        {
            return projectService.GetProjectNames();
        }
    }
}
