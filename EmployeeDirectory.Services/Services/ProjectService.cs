using EmployeeDirectory.Data.Data.Services;
using EmployeeDirectory.DATA;
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
    }
}
