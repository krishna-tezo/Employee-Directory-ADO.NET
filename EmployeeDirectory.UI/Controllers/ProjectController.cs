using EmployeeDirectory.Models.Models;
using EmployeeDirectory.Services.Services;
using EmployeeDirectory.UI.Interfaces;

namespace EmployeeDirectory.UI.Controllers
{
    public class ProjectController : IProjectController
    {
        private IProjectService projectService;

        public ProjectController(IProjectService projectService)
        {
            this.projectService = projectService;
        }

        public List<Project> ViewProjects()
        {
            return projectService.GetProjects();
        }


        public List<Tuple<string, string, string>> GetProjectNames()
        {
            return projectService.GetProjectNames();
        }


    }
}
