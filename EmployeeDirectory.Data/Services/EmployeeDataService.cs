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
        public EmployeeDataService(IDbConnection dbConnection) {
            this.dbConnection = dbConnection;
        }

        public List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                using (SqlConnection conn = dbConnection.GetConnection())
                {
                    string query = "SELECT * FROM Employee WHERE IsDeleted != '1'";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandType = CommandType.Text;

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            DateTime dob = DateTime.ParseExact(reader["DOB"].ToString(), "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                            DateTime joinDate = DateTime.ParseExact(reader["JoinDate"].ToString(), "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                            bool isDeleted = false;
                            if (reader["IsDeleted"].ToString() == "1")
                            {
                                isDeleted = true;
                            }
                            Employee employee = new Employee
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


                            employees.Add(employee);
                        }
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return employees;
        }
        public Employee GetEmployeeById(string id)
        {
            Employee employee = new Employee();
            try
            {
                string query = "SELECT * FROM EMPLOYEE WHERE ID = " + id;
                using (SqlConnection conn = dbConnection.GetConnection())
                {

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                        if (reader.Read())
                        {
                            DateTime dob = DateTime.ParseExact(reader.GetSqlValue(reader.GetOrdinal("DOB")).ToString(), "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                            DateTime joinDate = DateTime.ParseExact(reader.GetSqlValue(reader.GetOrdinal("JoinDate")).ToString(), "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                            bool isDeleted = false;
                            if (reader.GetSqlValue(reader.GetOrdinal("IsDeleted")).ToString() == "1")
                            {
                                isDeleted = true;
                            }
                            employee = new Employee
                            {
                                Id = reader.GetSqlValue(reader.GetOrdinal("Id")).ToString(),
                                FirstName = reader.GetSqlValue(reader.GetOrdinal("FirstName")).ToString(),
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
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return employee;
        }

        public void AddEmployee(Employee employee)
        {
            try
            {
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
                        int n = cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public int UpdateEmployee(Employee employee)
        {
            int rowsAffected = -1;
            try
            {
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return rowsAffected;
        }

        public int DeleteEmployee(string id)
        {
            int rowsAffected = -1;
            try
            {
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

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
