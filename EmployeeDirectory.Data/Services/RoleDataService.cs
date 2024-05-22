using EmployeeDirectory.Data.Data.Services;
using EmployeeDirectory.Models;
using Microsoft.Data.SqlClient;
namespace EmployeeDirectory.Data.Services
{
    public class RoleDataService : IRoleDataService
    {
        private IDbConnection dbConnection;
        private ICommonDataService commonDataServices;
        public RoleDataService(IDbConnection dbConnection, ICommonDataService commonDataServices)
        {
            this.dbConnection = dbConnection;
            this.commonDataServices = commonDataServices;
        }

        private Role MapRole(SqlDataReader reader)
        {
            return new Role
            {
                Id = reader["Id"].ToString(),
                Name = reader["Name"].ToString(),
                Department = reader["DeptName"].ToString(),
                Description = reader["Description"].ToString(),
                Location = reader["LocationName"].ToString()
            };
        }

        public List<Role> GetRoles()
        {
            string query = "SELECT " +
                           "R.Id, R.Name, R.Description, D.Name as DeptName, L.Name as LocationName " +
                           "FROM Role R " +
                           "JOIN Department D ON R.DeptId = D.Id " +
                           "JOIN Location L On R.LocationId = L.Id ";
            return commonDataServices.GetData(query, MapRole);
        }
        public Role GetRoleById(string id)
        {
            string query = "SELECT " +
                                "R.Id,R.Name,R.Description, D.Name as DeptName, L.Name as LocationName " +
                                "FROM Role R " +
                                "JOIN Department D ON R.DeptId = D.Id " +
                                "JOIN Location L On R.LocationId = L.Id " +
                                "WHERE R.Id = @Id";
            return commonDataServices.GetSingleData(query,id,MapRole);
        }

        public int Add(Role role, string departmentId, string locationId)
        {
            int rowsAffected = 0;
            using (SqlConnection conn = dbConnection.GetConnection())
            {
                string query = "INSERT INTO Role Values( " +
                    "@RoleId, @RoleName, @DepartmentId, @Description, @LocationId)";

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@RoleId", role.Id);
                    command.Parameters.AddWithValue("@RoleName", role.Name);
                    command.Parameters.AddWithValue("@DepartmentId", departmentId);
                    command.Parameters.AddWithValue("@LocationId", locationId);
                    command.Parameters.AddWithValue("@Description", role.Description);

                    conn.Open();
                    rowsAffected = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
            return rowsAffected;
        }

        public string GetDepartmentId(string departmentName)
        {
            string id = null;
            try
            {
                string query = "SELECT Id FROM Department WHERE Name = @DeptName";

                using (SqlConnection conn = dbConnection.GetConnection())
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@DeptName", departmentName);
                    conn.Open();
                    id = command.ExecuteScalar()?.ToString();
                    conn.Close();
                }
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
                using (SqlConnection conn = dbConnection.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT Id FROM Location WHERE Name = @LocationName";

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
                    query = "SELECT MAX(Id) FROM Location";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        string? result = cmd.ExecuteScalar()?.ToString();
                        if (result != null)
                        {
                            string newId = GenerateNewLocationId(result);
                            InsertLocation(newId, locationName);
                            return newId;
                        }
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "LOC001";
        }

        public string GenerateNewLocationId(string lastLocId)
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

        public int InsertLocation(string newLocationId, string newLocationName)
        {
            using (SqlConnection conn = dbConnection.GetConnection())
            {
                string insertQuery = "INSERT INTO Location (Id, Name) VALUES (@LocationId, @LocationName)";
                using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                {
                    insertCmd.Parameters.AddWithValue("@LocationId", newLocationId);
                    insertCmd.Parameters.AddWithValue("@LocationName", newLocationName);

                    conn.Open();
                    int rowsAffected = insertCmd.ExecuteNonQuery();
                    conn.Close();
                    return rowsAffected;
                }
            }
        }
    }


}