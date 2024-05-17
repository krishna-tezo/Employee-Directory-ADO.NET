using EmployeeDirectory.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EmployeeDirectory.Data.Data.Services
{
    public class RoleDataService : IRoleDataService
    {
        public List<Role> GetRoles()
        {
            List<Role> roles = new List<Role>();
            try
            {
                string query = "SELECT " +
                                "R.Id,R.Name,R.Description, D.DeptName, L.LocationName " +
                                "FROM Role R " +
                                "JOIN Department D ON R.DeptId = D.DeptId " +
                                "JOIN Location L On R.LocationId = L.LocationId ";

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, ConnectionHandler.GetConnection());

                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                foreach (DataRow row in dataTable.Rows)
                {
                    Role role = new Role
                    {
                        Id = row["Id"].ToString(),
                        Name = row["Name"].ToString(),
                        Department = row["DeptName"].ToString(),
                        Description = row["Description"].ToString(),
                        Location = row["LocationName"].ToString()
                    };
                    roles.Add(role);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return roles;
        }
        public Role GetRoleById(string id)
        {
            Role role = new Role();
            try
            {
                string query = "SELECT " +
                                "R.Id,R.Name,R.Description, D.DeptName, L.LocationName " +
                                "FROM Role R " +
                                "JOIN Department D ON R.DeptId = D.DeptId " +
                                "JOIN Location L On R.LocationId = L.LocationId " +
                                "WHERE R.Id = @Id";
                SqlCommand command = new SqlCommand(query, ConnectionHandler.GetConnection());
                command.Connection.Open();
                command.Parameters.AddWithValue("@id",id);

                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                if(reader.Read()) {
                    role = new Role
                    {
                        Id = reader["Id"].ToString(),
                        Name = reader["Name"].ToString(),
                        Department = reader["DeptName"].ToString(),
                        Description = reader["Description"].ToString(),
                        Location = reader["LocationName"].ToString()
                    };
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return role;
        }
    }
}
