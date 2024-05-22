using EmployeeDirectory.Data.Data.Services;
using EmployeeDirectory.Models;
using EmployeeDirectory.Models.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EmployeeDirectory.Data.Services
{
    public class ProjectDataService : IProjectDataService
    {
        private ICommonDataService commonDataService;
        public ProjectDataService( ICommonDataService commonDataService)
        {
            this.commonDataService = commonDataService;
        }
        private Project MapProject(SqlDataReader reader)
        {
            return new Project
            {
                Id = reader["Id"].ToString(),
                Name = reader["Name"].ToString(),
                ManagerName = reader["ManagerName"].ToString()
            };
        }
        public List<Project> GetProjects()
        {
            string query = "SELECT P.Id," +
                    "P.Name," +
                    "CONCAT(E.FirstName,' ',E.LastName) as ManagerName " +
                    "FROM Project P " +
                    "JOIN Manager M ON P.ManagerId = M.Id " +
                    "JOIN Employee E ON M.EmpId = E.Id ";
            return commonDataService.GetData(query, MapProject);
        }
        

        public Project GetProjectById(string id)
        {
            string query = "SELECT P.Id," +
                    "P.Name," +
                    "CONCAT(E.FirstName,' ',E.LastName) as ManagerName " +
                    "FROM Project P " +
                    "JOIN Manager M ON P.ManagerId = M.Id " +
                    "JOIN Employee E ON M.EmpId = E.Id " +
                    "WHERE P.Id = @id";

            return commonDataService.GetSingleData(query, id, MapProject);
        }
    }
}
