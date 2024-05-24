using EmployeeDirectory.Data.Data.Services;
using EmployeeDirectory.Data.SummaryModels;
using Microsoft.Data.SqlClient;
using System.Data;


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
        public List<EmployeeSummary> GetEmployeesSummary()
        {
            string query = "SELECT E.Id, E.FirstName, E.LastName, E.Email, E.DOB, E.MobileNumber, E.JoinDate, " +
                "E.ProjectId, P.Name as ProjectName, E.RoleId, R.Name as Role, D.Id as DepartmentId, D.Name as Department, " +
                "L.Id as LocationId, L.Name as Location, M.Id as ManagerId, CONCAT(K.FirstName,' ',K.LastName) as ManagerName " +
                "FROM Employee E " +
                "JOIN Role R ON E.RoleId = R.Id " +
                "JOIN Location L ON R.LocationId = L.Id " +
                "JOIN Department D ON R.DepartmentId = D.Id " +
                "JOIN Project P ON P.Id = E.ProjectId " +
                "JOIN Manager M ON P.ManagerId = M.Id " +
                "LEFT JOIN Employee K ON K.Id = M.EmpId";


            return commonDataServices.GetAll(query, commonDataServices.MapObject<EmployeeSummary>);
        }

        public EmployeeSummary GetEmployeeSummaryById(string id)
        {
            string query = "SELECT E.Id, E.FirstName, E.LastName, E.Email, E.DOB, E.MobileNumber, E.JoinDate, " +
                "E.ProjectId, P.Name as ProjectName, E.RoleId, R.Name as Role, D.Id as DepartmentId, D.Name as Department, " +
                "L.Id as LocationId, L.Name as Location, M.Id as ManagerId, CONCAT(K.FirstName,' ',K.LastName) as ManagerName " +
                "FROM Employee E " +
                "JOIN Role R ON E.RoleId = R.Id " +
                "JOIN Location L ON R.LocationId = L.Id " +
                "JOIN Department D ON R.DepartmentId = D.Id " +
                "JOIN Project P ON P.Id = E.ProjectId " +
                "JOIN Manager M ON P.ManagerId = M.Id " +
                "LEFT JOIN Employee K ON K.Id = M.EmpId WHERE Id = @Id";
            return commonDataServices.Get(query, id, commonDataServices.MapObject<EmployeeSummary>);
        }



        //public int AddEmployee(Employee employee)
        //{
        //    int rowsAffected = -1;
        //    using (SqlConnection conn = dbConnection.GetConnection())
        //    {
        //        string query = "INSERT INTO Employee VALUES (@Id, @FirstName, @LastName, @Email, @DOB, @MobileNumber, @JoinDate, @ProjectId, @RoleId, @IsDeleted)";

        //        using (SqlCommand cmd = new SqlCommand(query, conn))
        //        {
        //            cmd.CommandType = CommandType.Text;
        //            cmd.Parameters.AddWithValue("@Id", employee.Id);
        //            cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
        //            cmd.Parameters.AddWithValue("@LastName", employee.LastName);
        //            cmd.Parameters.AddWithValue("@Email", employee.Email);
        //            cmd.Parameters.AddWithValue("@DOB", employee.DOB);
        //            cmd.Parameters.AddWithValue("@MobileNumber", employee.MobileNumber);
        //            cmd.Parameters.AddWithValue("@JoinDate", employee.JoinDate);
        //            cmd.Parameters.AddWithValue("@ProjectId", employee.ProjectId);
        //            cmd.Parameters.AddWithValue("@RoleId", employee.RoleId);
        //            cmd.Parameters.AddWithValue("@IsDeleted", employee.IsDeleted);


        //            conn.Open();
        //            rowsAffected = cmd.ExecuteNonQuery();
        //            conn.Close();
        //        }

        //    }
        //    return rowsAffected;
        //}

        //public int UpdateEmployee(Employee employee)
        //{
        //    int rowsAffected = -1;

        //    using (SqlConnection conn = dbConnection.GetConnection())
        //    {
        //        string query = "UPDATE Employee SET " +
        //                        "FirstName = @FirstName, " +
        //                        "LastName = @LastName, " +
        //                        "Email = @Email, " +
        //                        "DOB = @DOB, " +
        //                        "MobileNumber = @MobileNumber, " +
        //                        "JoinDate = @JoinDate, " +
        //                        "ProjectId = @ProjectId, " +
        //                        "RoleId = @RoleId, " +
        //                        "IsDeleted = @IsDeleted " +
        //                        "WHERE Id = @Id";

        //        using (SqlCommand cmd = new SqlCommand(query, conn))
        //        {
        //            cmd.Parameters.AddWithValue("@Id", employee.Id);
        //            cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
        //            cmd.Parameters.AddWithValue("@LastName", employee.LastName);
        //            cmd.Parameters.AddWithValue("@Email", employee.Email);
        //            cmd.Parameters.AddWithValue("@DOB", employee.DOB);
        //            cmd.Parameters.AddWithValue("@MobileNumber", employee.MobileNumber);
        //            cmd.Parameters.AddWithValue("@JoinDate", employee.JoinDate);
        //            cmd.Parameters.AddWithValue("@ProjectId", employee.ProjectId);
        //            cmd.Parameters.AddWithValue("@RoleId", employee.RoleId);
        //            cmd.Parameters.AddWithValue("@IsDeleted", employee.IsDeleted);

        //            conn.Open();
        //            rowsAffected = cmd.ExecuteNonQuery();
        //            conn.Close();
        //        }
        //    }
        //    return rowsAffected;
        //}

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