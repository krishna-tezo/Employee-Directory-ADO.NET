using EmployeeDirectory.Models;

namespace EmployeeDirectory.Data.Data.Services
{
    public interface IEmployeeDataService
    {
        List<Employee> GetEmployees();
        Employee GetEmployeeById(string id);
        int UpdateEmployee(Employee employee);
        int DeleteEmployee(string id);
        void AddEmployee(Employee employee);
        string GetLastEmployeeId();
    }
}