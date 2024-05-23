//using EmployeeDirectory.Data.Data.Services;
//using Microsoft.Data.SqlClient;
//using System.Data;


//namespace EmployeeDirectory.Data.Services
//{
//    public class EmployeeDataService : IEmployeeDataService
//    {
//        private IDbConnection dbConnection;
//        private ICommonDataService commonDataServices;
//        public EmployeeDataService(IDbConnection dbConnection, ICommonDataService commonDataServices)
//        {
//            this.dbConnection = dbConnection;
//            this.commonDataServices = commonDataServices;
//        }

//        public int DeleteEmployee(string id)
//        {
//            int rowsAffected = -1;

//            using (SqlConnection conn = dbConnection.GetConnection())
//            {
//                string query = "UPDATE Employee SET IsDeleted = '1' WHERE Id=@Id";
//                using (SqlCommand cmd = new SqlCommand(query, conn))
//                {
//                    cmd.CommandType = CommandType.Text;

//                    cmd.Parameters.AddWithValue("@Id", id);
//                    conn.Open();
//                    rowsAffected = cmd.ExecuteNonQuery();
//                    conn.Close();
//                }
//            }
//            return rowsAffected;
//        }
        
//    }
//}