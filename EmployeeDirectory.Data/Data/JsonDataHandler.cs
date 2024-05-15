using EmployeeDirectory.Models;
using Newtonsoft.Json;

namespace EmployeeDirectory.DATA
{
    public class JsonDataHandler : IJsonDataHandler
    {
        public List<T> GetDataFromJson<T>()
        {
            string data;
            List<T>? dataList = [];
            try
            {
                if (typeof(T) == typeof(Employee))
                {
                    data = File.ReadAllText(Path.Combine("DATA", "employees.json"));
                    dataList = System.Text.Json.JsonSerializer.Deserialize<List<T>>(data);
                }
                else
                {
                    data = File.ReadAllText(Path.Combine("DATA", "roles.json"));
                    dataList = System.Text.Json.JsonSerializer.Deserialize<List<T>>(data);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (dataList == null)
            {
                return new List<T>();
            }
            return dataList;
        }

        public void UpdateDataToJson<T>(List<T> listData)
        {

            string updatedList = JsonConvert.SerializeObject(listData);
            string path;
            if (typeof(T) == typeof(Employee))
            {
                path = Path.Combine("DATA", "employees.json");
            }
            else
            {
                path = Path.Combine("DATA", "roles.json");
            }
            File.WriteAllText(path, updatedList);
        }
    }
}
