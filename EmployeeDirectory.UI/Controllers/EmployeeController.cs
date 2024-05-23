using EmployeeDirectory.Models;
using EmployeeDirectory.ViewModel;
using EmployeeDirectory.Models.Models;
using EmployeeDirectory.Services;
using EmployeeDirectory.UI.Interfaces;
namespace EmployeeDirectory.UI.Controllers
{
    public class EmployeeController : IEmployeeController
    {


        ICommonServices commonServices;
        public EmployeeController(ICommonServices commonServices)
        {
            this.commonServices = commonServices;
        }

        public ServiceResult<string> GetNewEmployeeId()
        {
            string empId = commonServices.GenerateNewId<Employee>().Data;
            if (empId == null)
            {
                empId = "TEZ00001";
            }
            return ServiceResult<string>.Success(empId);
        }
        public ServiceResult<EmployeeView> EmployeeViewMapper(List<Employee> employees, List<Role> roles, List<Project> projects, List<Department> departments, List<Manager> managers, List<Location> locations)
        {
            List<EmployeeView> employeesToView = employees
            .Join(roles, emp => emp.RoleId, role => role.Id, (emp, role) => new { Employee = emp, Role = role })
            .Join(projects, empRole => empRole.Employee.ProjectId, project => project.Id, (empRole, project) => new { empRole.Employee, empRole.Role, Project = project })
            .Join(departments, empRoleProj => empRoleProj.Role?.DepartmentId, department => department.Id, (empRoleProj, department) => new { empRoleProj.Employee, empRoleProj.Role, empRoleProj.Project, Department = department })
            .Join(managers, empRoleProjDep => empRoleProjDep.Project?.ManagerId, manager => manager.Id, (empRoleProjDep, manager) => new { empRoleProjDep.Employee, empRoleProjDep.Role, empRoleProjDep.Project, empRoleProjDep.Department, Manager = manager })
            .Join(locations, empRoleProjDepMan => empRoleProjDepMan.Role?.LocationId, location => location.Id, (empRoleProjDepMan, location) => new EmployeeView
            {
                Id = empRoleProjDepMan.Employee.Id,
                Name = $"{empRoleProjDepMan.Employee.FirstName} {empRoleProjDepMan.Employee.LastName}",
                Role = empRoleProjDepMan.Role?.Name,
                Department = empRoleProjDepMan.Department?.Name,
                Location = location?.Name,
                JoinDate = empRoleProjDepMan.Employee.JoinDate,
                ManagerName = empRoleProjDepMan.Manager?.Name,
                ProjectName = empRoleProjDepMan.Project?.Name
            }).ToList();

            if (employeesToView != null)
            {
                return ServiceResult<EmployeeView>.Success(employeesToView);
            }
            else
            {
                return ServiceResult<EmployeeView>.Fail("Error Occurred while joining");
            }
        }

        public ServiceResult<EmployeeView> ViewEmployees()
        {
            var employeeResult = commonServices.GetAll<Employee>();
            var roleResult = commonServices.GetAll<Role>();
            var projectResult = commonServices.GetAll<Project>();
            var departmentResult = commonServices.GetAll<Department>();
            var managerResult = commonServices.GetAll<Manager>();
            var locationresult = commonServices.GetAll<Location>();

            if (!employeeResult.IsOperationSuccess || !roleResult.IsOperationSuccess || !projectResult.IsOperationSuccess || !departmentResult.IsOperationSuccess || !managerResult.IsOperationSuccess || locationresult.IsOperationSuccess)
            {

                var errorMessages = new List<string>
                {
                    employeeResult.Message,
                    roleResult.Message,
                    projectResult.Message,
                    departmentResult.Message,
                    managerResult.Message,
                    locationresult.Message,
                }.Where(m => m != null).ToList();

                return ServiceResult<EmployeeView>.Fail(string.Join("; ", errorMessages));
            }

            List<Employee> employees = employeeResult.DataList;
            List<Role> roles = roleResult.DataList;
            List<Project> projects = projectResult.DataList;
            List<Department> departments = departmentResult.DataList;
            List<Manager> managers = managerResult.DataList;
            List<Location> locations = locationresult.DataList;

            return EmployeeViewMapper(employees, roles, projects, departments, managers, locations);
        }

        public ServiceResult<EmployeeView> ViewEmployee(string empId)
        {
            Employee employee = commonServices.Get<Employee>(empId).Data;
            Role role = commonServices.Get<Role>(employee.RoleId).Data;
            Project project = commonServices.Get<Project>(employee.ProjectId).Data;
            Department department = commonServices.Get<Department>(role.DepartmentId).Data;
            Manager manager = commonServices.Get<Manager>(project.ManagerId).Data;
            Location location = commonServices.Get<Location>(role.LocationId).Data;

            EmployeeView employeeToView = new EmployeeView();

            if (employee == null || role == null || project == null || department == null || manager == null || location == null)
            {
                return ServiceResult<EmployeeView>.Fail("Data not found");
            }
            {
                employeeToView = new EmployeeView
                {
                    Id = employee.Id,
                    Name = $"{employee.FirstName} {employee.LastName}",
                    Role = role.Name,
                    Department = department.Name,
                    Location = location.Name,
                    JoinDate = employee.JoinDate,
                    ManagerName = manager.Name,
                    ProjectName = project.Name
                };
            }
            return ServiceResult<EmployeeView>.Success(employeeToView);
        }

        public ServiceResult<Employee> GetEmployeeById(string id)
        {
            return commonServices.Get<Employee>(id);
        }

        public ServiceResult<int> AddEmployee(Employee employee)
        {
            return commonServices.Add<Employee>(employee);
        }

        public ServiceResult<int> EditEmployee(Employee employee)
        {
            return commonServices.Update(employee);
        }

        public ServiceResult<int> DeleteEmployee(string empId)
        {
            return commonServices.Delete<Employee>(empId);
        }

        public ServiceResult<List<Tuple<string, string>>> GetProjectNames()
        {
            try
            {
                List<Project> projects = commonServices.GetAll<Project>().DataList;
                List<Tuple<string, string>> projectDetails = projects
                    .Select(project => new { project.Id, project.Name })
                    .AsEnumerable()
                    .Select(project => new Tuple<string, string>(project.Id, project.Name))
                    .ToList();

                return ServiceResult<List<Tuple<string, string>>>.Success(projectDetails);
            }
            catch (Exception ex)
            {
                return ServiceResult<List<Tuple<string, string>>>.Fail(ex.Message);
            }
        }
    }
}