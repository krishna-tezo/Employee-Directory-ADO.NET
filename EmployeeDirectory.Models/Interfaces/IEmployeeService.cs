using EmployeeDirectory.Models;

namespace EmployeeDirectory.Interfaces
{
    public interface IEmployeeService
    {
        string GenerateNewId(string firstName, string lastName);
        Employee AddEmployee(Employee employee);
        List<Employee> GetEmployees();
        Employee? DeleteEmployee(string empId);
        Employee? GetEmployeeById(string id);
        Employee UpdateEmployee(Employee employee);

    }
}
