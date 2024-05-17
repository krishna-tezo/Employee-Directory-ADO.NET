using EmployeeDirectory.Data.Data.Services;
using EmployeeDirectory.Models;
using EmployeeDirectory.Models.Models;

namespace EmployeeDirectory.Services.Services
{
    public class ProjectService : IProjectService
    {
        private IProjectDataService projectDataService;
        public ProjectService(IProjectDataService projectDataService)
        {
            this.projectDataService = projectDataService;
        }

        public List<Project> GetProjects()
        {
            List<Project> projects = [];
            try
            {
                projects = projectDataService.GetProjects();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            return projects;
        }
        public Project GetProjectById(string id)
        {
            Project project = projectDataService.GetProjectById(id);
            return project;
        }

        public List<Tuple<string, string,string>> GetProjectNames()
        {
            List<Project> projects = GetProjects();
            List<Tuple<string, string,string>> projectDetails = projects.Select(project => new { project.Id, project.Name, project.ManagerName })
                                                    .AsEnumerable()
                                                    .Select(project => new Tuple<string, string,string>(project.Id, project.Name,project.ManagerName)).ToList();

            return projectDetails;

        }
    }
}
