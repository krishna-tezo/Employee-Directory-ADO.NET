using Microsoft.Data.SqlClient;
using System.Configuration;

namespace EmployeeDirectory.Data.Data
{
    public static class ConnectionHandler
    {
        public static SqlConnection GetConnection()
        {
            string connectionString = "Server=SQL-DEV;Database=KrishnaAnandDB;Trusted_Connection=True;TrustServerCertificate=True";
            //string connectionString = "Server=localhost;Port=3306;Database=KrishnaAnandDB;User ID=root;Password=krishna;SslMode=Required;";

            //connectionString = ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);

            return connection;
        }
    }
}
