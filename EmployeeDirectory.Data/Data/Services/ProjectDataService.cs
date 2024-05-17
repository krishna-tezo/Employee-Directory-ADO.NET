using EmployeeDirectory.Models.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EmployeeDirectory.Data.Data.Services
{
    public class ProjectDataService : IProjectDataService
    {
        public List<Project> GetProjects()
        {

            List<Project> projects = new List<Project>();
            try
            {
                string query = "SELECT P.ProjectId," +
                    "P.ProjectName," +
                    "CONCAT(E.FirstName,' ',E.LastName) as ManagerName " +
                    "FROM Project P " +
                    "JOIN Manager M ON P.ManagerId = M.ManagerId " +
                    "JOIN Employee E ON M.EmpId = E.Id ";

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, ConnectionHandler.GetConnection());

                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                foreach (DataRow row in dataTable.Rows)
                {
                    Project project = new Project
                    {
                        Id = row["ProjectId"].ToString(),
                        Name = row["ProjectName"].ToString(),
                        ManagerName = row["ManagerName"].ToString()
                    };
                    projects.Add(project);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return projects;
        }

        public Project GetProjectById(string id)
        {
            Project project = new Project();
            try
            {
                string query = "SELECT P.ProjectId," +
                    "P.ProjectName," +
                    "CONCAT(E.FirstName,' ',E.LastName) as ManagerName " +
                    "FROM Project P " +
                    "JOIN Manager M ON P.ManagerId = M.ManagerId " +
                    "JOIN Employee E ON M.EmpId = E.Id " +
                    "WHERE P.ProjectId = @id"
                    ;

                SqlCommand command = new SqlCommand(query, ConnectionHandler.GetConnection());
                command.Parameters.AddWithValue("@id", id);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                if(reader.Read())
                {
                    project = new Project
                    {
                        Id = reader["ProjectId"].ToString(),
                        Name = reader["ProjectName"].ToString(),
                        ManagerName = reader["ManagerName"].ToString()
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return project;
        }
    }
}
