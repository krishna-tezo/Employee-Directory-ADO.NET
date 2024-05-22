using EmployeeDirectory.Data.Data.Services;
using EmployeeDirectory.Models;
using EmployeeDirectory.Models.Models;
using EmployeeDirectory.Services.Services;
namespace EmployeeDirectory.Services
{
    public class ProjectService : IProjectService
    {
        private IProjectDataService projectDataService;
        public ProjectService(IProjectDataService projectDataService)
        {
            this.projectDataService = projectDataService;
        }

        public ServiceResult<Project> GetProjects()
        {
            try
            {
                List<Project> projects = projectDataService.GetProjects();
                if(projects.Count == 0)
                {
                    return ServiceResult<Project>.Fail("No Roles Found");
                }
                return ServiceResult<Project>.Success(projects);
            }
            catch (Exception ex)
            {
                return ServiceResult<Project>.Fail(ex.Message);
            }
        }

        public ServiceResult<Project> GetProjectById(string id)
        {
            try
            {
                Project project = projectDataService.GetProjectById(id);
                if(project == null)
                {
                    return ServiceResult<Project>.Fail("Project not Found");
                }
                return ServiceResult<Project>.Success(project);
            }
            catch (Exception ex)
            {
                return ServiceResult<Project>.Fail(ex.Message);
            }
        }

        public ServiceResult<List<Tuple<string, string, string>>> GetProjectNames()
        {
            try
            {
                List<Project> projects = GetProjects().DataList;
                List<Tuple<string, string, string>> projectDetails = projects
                    .Select(project => new { project.Id, project.Name, project.ManagerName })
                    .AsEnumerable()
                    .Select(project => new Tuple<string, string, string>(project.Id, project.Name, project.ManagerName))
                    .ToList();

                return ServiceResult<List<Tuple<string, string, string>>>.Success(projectDetails);
            }
            catch (Exception ex)
            {
                return ServiceResult<List<Tuple<string, string, string>>>.Fail(ex.Message);
            }
        }
    }
}
