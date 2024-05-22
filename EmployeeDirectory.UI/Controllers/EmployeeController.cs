using EmployeeDirectory.UI.Interfaces;
using EmployeeDirectory.Models;
using EmployeeDirectory.ViewModel;
using EmployeeDirectory.Interfaces;
using AutoMapper;
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
        public EmployeeController(IEmployeeService employeeService, IRoleService roleService,IProjectService projectService)
        {
            this.employeeService = employeeService;
            this.roleService = roleService;
            this.projectService = projectService;
        }

        public string GetNewEmployeeId(string firstName, string lastName)
        {
            return employeeService.GenerateNewId(firstName, lastName).Data;
        }

        public Mapper GetEmployeeViewMapper()
        {
            MapperConfiguration config = new(cfg =>
            {
                cfg.CreateMap<Employee, EmployeeView>()
                .ForMember(dest => dest.Name, act => act.MapFrom(src => src.FirstName + " " + src.LastName));

                cfg.CreateMap<Role, EmployeeView>()
                .ForMember(dest => dest.Role, act => act.MapFrom(src => src.Name))
                .ForMember(dest => dest.Id, act => act.Ignore())
                .ForMember(dest => dest.Name, act => act.Ignore());

                cfg.CreateMap<Project, EmployeeView>()
                .ForMember(dest => dest.ProjectName, act => act.MapFrom(src => src.Name))
                .ForMember(dest => dest.ManagerName, act => act.MapFrom(src => src.ManagerName))
                .ForMember(dest => dest.Id, act => act.Ignore());
            });

            return new Mapper(config);
        }


        public List<EmployeeView> ViewEmployees()
        {

            Mapper mapper = GetEmployeeViewMapper();
            List<Employee> employees = employeeService.GetEmployees().DataList;
            List<Role> roles = roleService.GetAllRoles().DataList;
            List<Project> projects = projectService.GetProjects().DataList;

            //List<EmployeeView> employeesToView = employees.Join(roles, emp => emp.RoleId, role => role.Id, (employee, role) =>
            //{
            //    EmployeeView employeeToView = mapper.Map<Employee, EmployeeView>(employee);
            //    employeeToView = mapper.Map(role, employeeToView);
            //    return employeeToView;
            //}).ToList();



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
            return employeesToView;
        }

        public EmployeeView? ViewEmployee(string empId)
        {
            Mapper mapper = GetEmployeeViewMapper();

            Employee? employee = employeeService.GetEmployeeById(empId).Data;

            EmployeeView? employeeToView = new EmployeeView();
            if (employee == null)
            {
                return null;
            }
            else
            {   

                Project? project = projectService.GetProjectById(employee.ProjectId).Data;
                Role role = roleService.GetRoleById(employee.RoleId!).Data;
                if (role == null)
                {
                    return null;
                }
                else
                {
                    //employeeToView = mapper.Map<Employee, EmployeeView>(employee);
                    //employeeToView = mapper.Map(role, employeeToView);
                    employeeToView = new EmployeeView
                    {
                        Id = employee.Id,
                        Name = employee.FirstName + " " + employee.LastName,
                        Role = role.Name,
                        Department = role.Department,
                        Location = role.Location,
                        JoinDate = employee.JoinDate,
                        ManagerName = project.ManagerName,
                        ProjectName = project.Name
                    };
                }
            }
            return employeeToView;
        }

        public Employee? GetEmployeeById(string id)
        {
            return employeeService.GetEmployeeById(id).Data;
        }

        public int AddEmployee(Employee employee)
        {
            return employeeService.AddEmployee(employee).Data;
        }

        public int EditEmployee(Employee employee)
        {
            return employeeService.UpdateEmployee(employee).Data;
        }

        public int DeleteEmployee(string empId)
        {
            return employeeService.DeleteEmployee(empId).Data;
        }
    }
}
