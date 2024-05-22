using EmployeeDirectory.Models;

namespace EmployeeDirectory.Services
{
    public interface IEmployeeService
    {
        ServiceResult<Employee> GetEmployees();
        ServiceResult<Employee> GetEmployeeById(string id);
        ServiceResult<int> UpdateEmployee(Employee newEmployee);
        ServiceResult<int> AddEmployee(Employee employee);
        ServiceResult<int> DeleteEmployee(string empId);
        ServiceResult<string> GenerateNewId(string firstName, string lastName);
    }
}