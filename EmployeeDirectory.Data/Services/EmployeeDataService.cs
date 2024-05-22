using EmployeeDirectory.Data.Data.Services;
using EmployeeDirectory.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Globalization;


namespace EmployeeDirectory.Data.Services
{
    public class EmployeeDataService : IEmployeeDataService
    {
        private IDbConnection dbConnection;
        private ICommonDataService commonDataServices;
        public EmployeeDataService(IDbConnection dbConnection, ICommonDataService commonDataServices)
        {
            this.dbConnection = dbConnection;
            this.commonDataServices = commonDataServices;
        }

        private Employee MapEmployee(SqlDataReader reader)
        {
            DateTime dob = DateTime.ParseExact(reader["DOB"].ToString(), "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
            DateTime joinDate = DateTime.ParseExact(reader["JoinDate"].ToString(), "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
            bool isDeleted = reader["IsDeleted"].ToString() == "1";

            return new Employee
            {
                Id = reader["Id"].ToString(),
                FirstName = reader["FirstName"].ToString(),
                LastName = reader["LastName"].ToString(),
                Email = reader["Email"].ToString(),
                DOB = dob,
                MobileNumber = reader["MobileNumber"].ToString(),
                JoinDate = joinDate,
                ProjectId = reader["ProjectId"].ToString(),
                RoleId = reader["RoleId"].ToString(),
                IsDeleted = isDeleted
            };
        }

        public List<Employee> GetEmployees()
        {
            string query = "SELECT * FROM Employee WHERE IsDeleted != '1'";
            return commonDataServices.GetData(query, MapEmployee);
        }

        public Employee GetEmployeeById(string id)
        {
            string query = $"SELECT * FROM EMPLOYEE WHERE ID = @id";
            return commonDataServices.GetSingleData(query, id, MapEmployee);
        }



        public int AddEmployee(Employee employee)
        {
            int rowsAffected = -1;
            using (SqlConnection conn = dbConnection.GetConnection())
            {
                string query = "INSERT INTO Employee VALUES (@Id, @FirstName, @LastName, @Email, @DOB, @MobileNumber, @JoinDate, @ProjectId, @RoleId, @IsDeleted)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Id", employee.Id);
                    cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                    cmd.Parameters.AddWithValue("@Email", employee.Email);
                    cmd.Parameters.AddWithValue("@DOB", employee.DOB);
                    cmd.Parameters.AddWithValue("@MobileNumber", employee.MobileNumber);
                    cmd.Parameters.AddWithValue("@JoinDate", employee.JoinDate);
                    cmd.Parameters.AddWithValue("@ProjectId", employee.ProjectId);
                    cmd.Parameters.AddWithValue("@RoleId", employee.RoleId);
                    cmd.Parameters.AddWithValue("@IsDeleted", employee.IsDeleted);


                    conn.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }

            }
            return rowsAffected;
        }

        public int UpdateEmployee(Employee employee)
        {
            int rowsAffected = -1;

            using (SqlConnection conn = dbConnection.GetConnection())
            {
                string query = "UPDATE Employee SET " +
                                "FirstName = @FirstName, " +
                                "LastName = @LastName, " +
                                "Email = @Email, " +
                                "DOB = @DOB, " +
                                "MobileNumber = @MobileNumber, " +
                                "JoinDate = @JoinDate, " +
                                "ProjectId = @ProjectId, " +
                                "RoleId = @RoleId, " +
                                "IsDeleted = @IsDeleted " +
                                "WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", employee.Id);
                    cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                    cmd.Parameters.AddWithValue("@Email", employee.Email);
                    cmd.Parameters.AddWithValue("@DOB", employee.DOB);
                    cmd.Parameters.AddWithValue("@MobileNumber", employee.MobileNumber);
                    cmd.Parameters.AddWithValue("@JoinDate", employee.JoinDate);
                    cmd.Parameters.AddWithValue("@ProjectId", employee.ProjectId);
                    cmd.Parameters.AddWithValue("@RoleId", employee.RoleId);
                    cmd.Parameters.AddWithValue("@IsDeleted", employee.IsDeleted);

                    conn.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            return rowsAffected;
        }

        public int DeleteEmployee(string id)
        {
            int rowsAffected = -1;

            using (SqlConnection conn = dbConnection.GetConnection())
            {
                string query = "UPDATE Employee SET IsDeleted = '1' WHERE Id=@Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@Id", id);
                    conn.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            return rowsAffected;
        }
        public string GetLastEmployeeId()
        {
            string? result = null;
            using (SqlConnection conn = dbConnection.GetConnection())
            {
                conn.Open();
                string query = "SELECT MAX(id) FROM Employee";
                using (SqlCommand cmd = new(query, conn))
                {
                    //TODO: Shift the new id to Service Layer
                    result = cmd.ExecuteScalar().ToString();
                    if (result == null)
                        return "TEZ00001";  // If no employees exist, start with ID 1
                }
                conn.Close();
                return result;
            }
        }
    }
}