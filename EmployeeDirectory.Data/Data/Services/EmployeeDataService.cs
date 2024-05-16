using EmployeeDirectory.Interfaces;
using EmployeeDirectory.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Globalization;


namespace EmployeeDirectory.Data.Data.Services
{
    public class EmployeeDataService : IEmployeeDataService
    {
        public List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                string query = "SELECT * FROM EMPLOYEE";

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, ConnectionHandler.GetConnection());

                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                foreach (DataRow row in dataTable.Rows)
                {
                    DateTime dob = DateTime.ParseExact(row["DOB"].ToString(), "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                    DateTime joinDate = DateTime.ParseExact(row["JoinDate"].ToString(), "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                    bool isDeleted = false;
                    if (row["IsDeleted"].ToString() == "1")
                    {
                        isDeleted = true;
                    }
                    Employee employee = new Employee
                    {
                        Id = row["Id"].ToString(),
                        FirstName = row["FirstName"].ToString(),
                        LastName = row["LastName"].ToString(),
                        Email = row["Email"].ToString(),
                        DOB = dob,
                        MobileNumber = row["MobileNumber"].ToString(),
                        JoinDate = joinDate,
                        ProjectId = row["ProjectId"].ToString(),
                        RoleId = row["RoleId"].ToString(),
                        IsDeleted = isDeleted
                    };
                    employees.Add(employee);
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
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT* FROM EMPLOYEE WHERE ID = "+id;
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return employee;
        }

    }
}
