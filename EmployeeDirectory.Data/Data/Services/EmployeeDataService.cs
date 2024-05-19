using EmployeeDirectory.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Globalization;


namespace EmployeeDirectory.Data.Data.Services
{
    public class EmployeeDataService : IEmployeeDataService
    {
        //TODO: Update the emp data with new id
        /*
            INSERT INTO Employee VALUES ('TEZ00001', 'Edgar', 'Jones', 'edgar@tezo.com', '2002-02-12', '9999999999', '2023-03-12', 'PR0001', 'RL0001');
            INSERT INTO Employee VALUES ('TEZ00002', 'Kensley', 'Curry', 'edgar@tezo.com', '2002-02-12', '9999999999', '2023-03-12', 'PR0002', 'RL0002');
            INSERT INTO Employee VALUES ('TEZ00003', 'Liam', 'Davis', 'liam@tezo.com', '1993-08-27', '6666666666', '2023-03-12', 'PR0003', 'RL0003');
            INSERT INTO Employee VALUES ('TEZ00004', 'Fernanda', 'Yu', 'sophia@tezo.com', '1985-09-30', '7777777777', '2023-03-12', 'PR0004', 'RL0004');
            INSERT INTO Employee VALUES ('TEZ00005', 'Nathan', 'Smith', 'nathan@tezo.com', '1990-04-15', '8888888888', '2023-03-12', 'PR0006', 'RL0004');
            INSERT INTO Employee VALUES ('TEZ00006', 'Emily', 'Johnson', 'emily@tezo.com', '1988-09-21', '7777777777', '2023-03-12', 'PR0002', 'RL0005');
            INSERT INTO Employee VALUES ('TEZ00007', 'Ella', 'Wilson', 'ella@tezo.com', '1991-05-18', '4444444444', '2023-03-12', 'PR0002', 'RL0006');
            INSERT INTO Employee VALUES ('TEZ00008', 'Olivia', 'Taylor', 'olivia@tezo.com', '1996-11-05', '5555555555', '2023-03-12', 'PR0006', 'RL0003');
            INSERT INTO Employee VALUES ('TEZ00009', 'Briggs', 'Peterson', 'alice@tezo.com', '1990-05-18', '8888888888', '2023-03-12', 'PR0002', 'RL0005');
            INSERT INTO Employee VALUES ('TEZ00010', 'Ethan', 'Brown', 'ethan@tezo.com', '1987-12-10', '3333333333', '2023-03-12', 'PR0004', 'RL0001');
            INSERT INTO Employee VALUES ('TEZ00011', 'Badgar', 'Smith', 'alice@tezo.com', '1990-05-18', '8888888888', '2023-03-12', 'PR0004', 'RL0004');
            INSERT INTO Employee VALUES ('TEZ00012', 'Ava', 'Jones', 'ava@tezo.com', '1984-03-26', '2222222222', '2023-03-12', 'PR0002', 'RL0006');
            INSERT INTO Employee VALUES ('TEZ00013', 'Badger', 'Johnson', 'sophia@tezo.com', '1985-09-30', '7777777777', '2023-03-12', 'PR0001', 'RL0002');
            INSERT INTO Employee VALUES ('TEZ00014', 'Cadger', 'Williams', 'michael@tezo.com', '1993-07-22', '6666666666', '2023-03-12', 'PR0003', 'RL0001');
            INSERT INTO Employee VALUES ('TEZ00015', 'Cadero', 'Williamso', 'michael@tezo.com', '1993-07-22', '6666666666', '2023-03-12', 'PR0001', 'RL0005');
            INSERT INTO Employee VALUES ('TEZ00016', 'Olivia', 'Martinez', 'olivia@tezo.com', '1996-04-28', '4444444444', '2023-03-12', 'PR0002', 'RL0002');
            INSERT INTO Employee VALUES ('TEZ00017', 'Dangelo', 'Navarro', 'edgar@tezo.com', '2002-02-12', '9999999999', '2023-03-12', 'PR0004', 'RL0003');
            INSERT INTO Employee VALUES ('TEZ00018', 'Winter', 'Franco', 'edgar@tezo.com', '2002-02-12', '9999999999', '2023-03-12', 'PR0003', 'RL0006');
            INSERT INTO Employee VALUES ('TEZ00019', 'Reign', 'Colon', 'edgar@tezo.com', '2002-02-12', '9999999999', '2023-03-12', 'PR0002', 'RL0002');
         */
        //TODO: Change the new employee id
        /*
        
         */

        
        public List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                string query = "SELECT * FROM Employee WHERE IsDeleted != '1'";

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

        public void AddEmployee(Employee employee)
        {
            try
            {
                using (SqlConnection conn = ConnectionHandler.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO Employee VALUES (@Id, @FirstName, @LastName, @Email, @DOB, @MobileNumber, @JoinDate, @ProjectId, @RoleId, @IsDeleted)";
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
                SqlConnection connection = ConnectionHandler.GetConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;

                cmd.CommandType = CommandType.Text;
                cmd.CommandText =
                    "UPDATE Employee SET " +
                    "FirstName = @FirstName, " +
                    "LastName = @LastName, " +
                    "Email = @Email, "+
                    "DOB = @DOB, " +
                    "MobileNumber = @MobileNumber, " +
                    "JoinDate = @JoinDate, " +
                    "ProjectId = @ProjectId, " +
                    "RoleId = @RoleId, " +
                    "IsDeleted = @IsDeleted " +
                    "WHERE Id = @Id";

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

                connection.Open();
                rowsAffected = cmd.ExecuteNonQuery();
                connection.Close();
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
                SqlConnection connection = ConnectionHandler.GetConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE Employee SET IsDeleted = '1' WHERE Id=@Id";
                cmd.Parameters.AddWithValue("@Id", id);

                connection.Open();
                rowsAffected = cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return rowsAffected;
        }
        public static string GetLastEmployeeId()
        {
            using (SqlConnection conn = ConnectionHandler.GetConnection())
            {
                conn.Open();
                string query = "SELECT MAX(id) FROM Employee";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;
                string result = cmd.ExecuteScalar().ToString();
                if (result == null)
                    return "TEZ00001";  // If no employees exist, start with ID 1
                conn.Close();
                return result;
            }
        }
        
    }
}
