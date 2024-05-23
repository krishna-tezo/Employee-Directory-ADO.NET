using EmployeeDirectory.Models;
using EmployeeDirectory.ViewModel;

namespace EmployeeDirectory.UI.Controllers
{
    public interface IEmployeeController
    {
        ServiceResult<int> AddEmployee(Employee employee);
        ServiceResult<int> DeleteEmployee(string empId);
        ServiceResult<int> EditEmployee(Employee employee);
        ServiceResult<Employee> GetEmployeeById(string id);
        ServiceResult<string> GetNewEmployeeId(string firstName, string lastName);
        ServiceResult<EmployeeView> ViewEmployee(string empId);
        ServiceResult<EmployeeView> ViewEmployees();
    }
}