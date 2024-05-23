using Microsoft.Data.SqlClient;
using System.Globalization;

namespace EmployeeDirectory.Data.Services
{
    public class CommonDataService : ICommonDataService
    {
        private IDbConnection dbConnection;
        public CommonDataService(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        //Get All Data
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


        public T MapObject<T>(SqlDataReader reader) where T : new()
        {
            T obj = new();
            var properties = typeof(T).GetProperties();

            foreach (var prop in properties)
            {
                if (reader[prop.Name] == DBNull.Value)
                    continue;

                object value = reader[prop.Name];
                var propType = prop.PropertyType;

                if (propType == typeof(DateTime) && DateTime.TryParseExact(value.ToString(), "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateValue))
                {
                    prop.SetValue(obj, dateValue);
                }
                else if (propType == typeof(bool))
                {
                    if (value.ToString() == "1")
                    {
                        prop.SetValue(obj, true);
                    }
                    else if(value.ToString() == "0")
                    {
                        prop.SetValue(obj, false);
                    }
                }
                else
                {
                    prop.SetValue(obj, Convert.ChangeType(value, propType));
                }
            }
            return obj;
        }
    }
}
