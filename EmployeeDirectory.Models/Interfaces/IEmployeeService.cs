using EmployeeDirectory.Models;

namespace EmployeeDirectory.Interfaces
{
    public interface IEmployeeService
    {
        string GenerateNewId(string firstName, string lastName);
        Employee AddEmployee(Employee employee);
        List<Employee> GetEmployees();
        int DeleteEmployee(string empId);
        Employee? GetEmployeeById(string id);
        int UpdateEmployee(Employee employee);

    }
}
