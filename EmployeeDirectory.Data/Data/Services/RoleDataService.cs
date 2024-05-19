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
                command.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                if (reader.Read())
                {
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

        public int Add(Role role, string locationId, string departmentId)
        {   

            using(SqlConnection conn = ConnectionHandler.GetConnection())
            {
                string query = "INSERT INTO Role Values " +
                    "@RoleId, @RoleName, @DepartmentId, @LocationId, @Desription";
                SqlCommand command = new SqlCommand(query, ConnectionHandler.GetConnection());
                command.Connection.Open();
                command.Parameters.AddWithValue("@RoleId", role.Id);
                command.Parameters.AddWithValue("@RoleName", role.Name);
                command.Parameters.AddWithValue("@DepartmentId", departmentId);
                command.Parameters.AddWithValue("@LocationId", departmentId);
                command.Parameters.AddWithValue("@Description", role.Description);


            }
        }


        public string GetDepartmentId(string departmentName)
        {
            string id = null;
            try
            {
                string query = "SELECT DeptId FROM Department WHERE DeptName = @DeptName";

                SqlCommand command = new SqlCommand(query, ConnectionHandler.GetConnection());
                command.Connection.Open();
                command.Parameters.AddWithValue("@DeptName", departmentName);

                id = command.ExecuteScalar().ToString();
                command.Connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return id;
        }


        public string GetLocationId(string locationName)
        {
            try
            {
                using (SqlConnection conn = ConnectionHandler.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT LocationId FROM Location WHERE LocationName = @LocationName";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@LocationName", locationName);
                        var result = command.ExecuteScalar();
                        if (result != null)
                        {
                            return result.ToString();
                        }
                    }

                    // If the location does not exist, get the maximum LocationId
                    query = "SELECT MAX(LocationId) FROM Location";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        string? result = cmd.ExecuteScalar().ToString();
                        if (result != null)
                        {
                            string newId = GenerateNewLocationId(result?.ToString());
                            InsertLocation(newId, locationName);
                            return newId;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "LOC001";

        }



        public static string GenerateNewLocationId(string lastLocId)
        {
            string prefix = "LOC";
            string numericPart = lastLocId.Substring(prefix.Length);

            if (int.TryParse(numericPart, out int numericId))
            {
                int newNumericId = numericId + 1;
                return prefix + newNumericId.ToString("D3");
            }
            else
            {
                throw new ArgumentException("Invalid location ID format.");
            }
        }

        public static void InsertLocation(string newLocationId, string newLocationName)
        {
            using (SqlConnection conn = ConnectionHandler.GetConnection())
            {
                conn.Open();
                string insertQuery = "INSERT INTO Location (LocationId, LocationName) VALUES (@LocationId, @LocationName)";
                using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                {
                    insertCmd.Parameters.AddWithValue("@LocationId", newLocationId);
                    insertCmd.Parameters.AddWithValue("@LocationName", newLocationName);
                    insertCmd.ExecuteNonQuery();
                }
            }
        }
    }
}