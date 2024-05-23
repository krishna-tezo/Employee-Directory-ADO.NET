using EmployeeDirectory.Models;
using EmployeeDirectory.Models.Models;
using EmployeeDirectory.ViewModel;

namespace EmployeeDirectory.UI.Interfaces
{
    public interface IEmployeeController
    {
        ServiceResult<int> AddEmployee(Employee employee);
        ServiceResult<int> DeleteEmployee(string empId);
        ServiceResult<int> EditEmployee(Employee employee);
        ServiceResult<EmployeeView> EmployeeViewMapper(List<Employee> employees, List<Role> roles, List<Project> projects, List<Department> departments, List<Manager> managers, List<Location> locations);
        ServiceResult<Employee> GetEmployeeById(string id);
        ServiceResult<string> GetNewEmployeeId();
        ServiceResult<List<Tuple<string, string>>> GetProjectNames();
        ServiceResult<EmployeeView> ViewEmployee(string empId);
        ServiceResult<EmployeeView> ViewEmployees();
    }
}