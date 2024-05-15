
namespace EmployeeDirectory.DATA
{
    public interface IJsonDataHandler
    {
        List<T> GetDataFromJson<T>();
        void UpdateDataToJson<T>(List<T> listData);
    }
}