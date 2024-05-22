using Microsoft.Data.SqlClient;

namespace EmployeeDirectory.Data.Services
{
    public interface ICommonDataService
    {
        List<T> GetData<T>(string query, Func<SqlDataReader, T> mapFunction);
        T GetSingleData<T>(string query,string id, Func<SqlDataReader, T> mapFunction);
    }
}