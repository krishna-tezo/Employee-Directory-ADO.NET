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
                using (SqlConnection conn = ConnectionHandler.GetConnection())
                {
                    string query = "SELECT P.Id," +
                        "P.Name," +
                        "CONCAT(E.FirstName,' ',E.LastName) as ManagerName " +
                        "FROM Project P " +
                        "JOIN Manager M ON P.ManagerId = M.Id " +
                        "JOIN Employee E ON M.EmpId = E.Id ";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while(reader.Read())
                        {
                            Project project = new Project
                            {
                                Id = reader["Id"].ToString(),
                                Name = reader["Name"].ToString(),
                                ManagerName = reader["ManagerName"].ToString()
                            };
                            projects.Add(project);
                        }
                        conn.Close();
                    }
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
                string query = "SELECT P.Id," +
                        "P.Name," +
                        "CONCAT(E.FirstName,' ',E.LastName) as ManagerName " +
                        "FROM Project P " +
                        "JOIN Manager M ON P.ManagerId = M.Id " +
                        "JOIN Employee E ON M.EmpId = E.Id " +
                        "WHERE P.Id = @id";

                using (SqlConnection conn = ConnectionHandler.GetConnection())
                {
                    using (SqlCommand cmd = new(query, conn))
                    {
                        conn.Open();
                        cmd.Parameters.AddWithValue("@id", id);

                        SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                        if (reader.Read())
                        {
                            project = new Project
                            {
                                Id = reader["Id"].ToString(),
                                Name = reader["Name"].ToString(),
                                ManagerName = reader["ManagerName"].ToString()
                            };
                        }
                        conn.Close();
                    }
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
