using EmployeeDirectory.Models;
using EmployeeDirectory.Models.Models;
using EmployeeDirectory.ViewModel;

namespace EmployeeDirectory.Interfaces
{
    public interface IUIService
    {
        void AddEmployee();
        void AddRole();
        void DeleteEmployee();
        void EditEmployee();
        Employee GetEmployeeDetailsFromConsole(Employee employee, EmployeeFormType formType, string empId = "");
        //string GetEmployeeRoleDetails(string parameter, string department = "", string roleName = "");
        void ShowEmployeesDataInTabularFormat(List<EmployeeView> employees);
        void ShowRolesDataInTabularFormat(List<Role> roles);
        void ViewAllRoles();
        void ViewEmployee();
        void ViewEmployees();
    }
}