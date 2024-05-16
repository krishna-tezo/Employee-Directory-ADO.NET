using EmployeeDirectory.Models;

namespace EmployeeDirectory.Data.Data.Services
{
    public interface IEmployeeDataService
    {
        List<Employee> GetEmployees();
    }
}