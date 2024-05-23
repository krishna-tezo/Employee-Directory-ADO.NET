using EmployeeDirectory.Models;
using EmployeeDirectory.ViewModel;
using EmployeeDirectory.Models.Models;
using EmployeeDirectory.Services.Services;
using EmployeeDirectory.Services;
namespace EmployeeDirectory.UI.Controllers
{
    public class EmployeeController : IEmployeeController
    {
        IEmployeeService employeeService;
        IRoleService roleService;
        IProjectService projectService;
        public EmployeeController(IEmployeeService employeeService, IRoleService roleService, IProjectService projectService)
        {
            this.employeeService = employeeService;
            this.roleService = roleService;
            this.projectService = projectService;
        }

        public ServiceResult<string> GetNewEmployeeId(string firstName, string lastName)
        {
            return employeeService.GenerateNewId(firstName, lastName);
        }

        public ServiceResult<EmployeeView> ViewEmployees()
        {
            var employeeResult = employeeService.GetEmployees();
            var roleResult = roleService.GetAllRoles();
            var projectResult = projectService.GetProjects();

            if (!employeeResult.IsOperationSuccess || !roleResult.IsOperationSuccess || !projectResult.IsOperationSuccess)
            {

                var errorMessages = new List<string>
                {
                    employeeResult.Message,
                    roleResult.Message,
                    projectResult.Message
                }.Where(m => m != null).ToList();

                return ServiceResult<EmployeeView>.Fail(string.Join("; ", errorMessages));
            }

            List<Employee> employees = employeeResult.DataList;
            List<Role> roles = roleResult.DataList;
            List<Project> projects = projectResult.DataList;

            List<EmployeeView> employeesToView = employees
                .Join(roles, emp => emp.RoleId, role => role.Id, (emp, role) => new { Employee = emp, Role = role })
                .Join(projects, empRole => empRole.Employee.ProjectId, project => project.Id, (empRole, project) => new EmployeeView
                {
                    Id = empRole.Employee.Id,
                    Name = $"{empRole.Employee.FirstName} {empRole.Employee.LastName}",
                    Role = empRole.Role.Name,
                    Department = empRole.Role.Department,
                    Location = empRole.Role.Location,
                    JoinDate = empRole.Employee.JoinDate,
                    ManagerName = project.ManagerName,
                    ProjectName = project.Name
                }).ToList();

            return ServiceResult<EmployeeView>.Success(employeesToView);
        }

        public ServiceResult<EmployeeView> ViewEmployee(string empId)
        {
            var employeeResult = employeeService.GetEmployeeById(empId);

            if (!employeeResult.IsOperationSuccess)
            {
                return ServiceResult<EmployeeView>.Fail(employeeResult.Message);
            }

            Employee? employee = employeeResult.Data;
            if (employee == null)
            {
                return ServiceResult<EmployeeView>.Fail("Employee not found");
            }

            var projectResult = projectService.GetProjectById(employee.ProjectId);
            var roleResult = roleService.GetRoleById(employee.RoleId);

            if (!projectResult.IsOperationSuccess || !roleResult.IsOperationSuccess)
            {
                var errorMessages = new List<string>
                {
                    projectResult.Message,
                    roleResult.Message
                }.Where(m => m != null).ToList();

                return ServiceResult<EmployeeView>.Fail(string.Join("; ", errorMessages));
            }

            Project? project = projectResult.Data;
            Role role = roleResult.Data;

            if (project == null || role == null)
            {
                return ServiceResult<EmployeeView>.Fail("Project or Role not found");
            }

            EmployeeView employeeToView = new EmployeeView
            {
                Id = employee.Id,
                Name = $"{employee.FirstName} {employee.LastName}",
                Role = role.Name,
                Department = role.Department,
                Location = role.Location,
                JoinDate = employee.JoinDate,
                ManagerName = project.ManagerName,
                ProjectName = project.Name
            };

            return ServiceResult<EmployeeView>.Success(employeeToView);
        }

        public ServiceResult<Employee> GetEmployeeById(string id)
        {
            return employeeService.GetEmployeeById(id);
        }

        public ServiceResult<int> AddEmployee(Employee employee)
        {
            return employeeService.AddEmployee(employee);
        }

        public ServiceResult<int> EditEmployee(Employee employee)
        {
            return employeeService.UpdateEmployee(employee);
        }

        public ServiceResult<int> DeleteEmployee(string empId)
        {
            return employeeService.DeleteEmployee(empId);
        }
    }
}
