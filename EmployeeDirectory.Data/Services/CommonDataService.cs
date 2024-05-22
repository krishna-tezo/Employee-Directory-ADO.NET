using Microsoft.Data.SqlClient;

namespace EmployeeDirectory.Data.Services
{
    public class CommonDataService : ICommonDataService
    {
        private IDbConnection dbConnection;
        public CommonDataService(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }
        public List<T> GetData<T>(string query, Func<SqlDataReader, T> mapFunction)
        {
            List<T> data = new List<T>();

            using (SqlConnection conn = dbConnection.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        T item = mapFunction(reader);
                        data.Add(item);
                    }
                }
            }
            return data;
        }

        public T GetSingleData<T>(string query, string id, Func<SqlDataReader, T> mapFunction)
        {
            T? data = default;

            using (SqlConnection conn = dbConnection.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        T item = mapFunction(reader);
                        data = item;
                    }
                }
            }
            return data;
        }
    }
}
