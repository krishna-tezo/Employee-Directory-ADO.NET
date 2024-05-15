using EmployeeDirectory.UI.Interfaces;
using EmployeeDirectory.Models;
using EmployeeDirectory.ViewModel;
using EmployeeDirectory.Interfaces;
using AutoMapper;
using System.Data;
namespace EmployeeDirectory.UI.Controllers
{
    public class EmployeeController : IEmployeeController
    {
        IEmployeeService employeeService;
        IRoleService roleService;

        public EmployeeController(IEmployeeService employeeService, IRoleService roleService)
        {
            this.employeeService = employeeService;
            this.roleService = roleService;
        }

        public string GetNewEmployeeId(string firstName, string lastName)
        {
            return employeeService.GenerateNewId(firstName,lastName);
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
            });
           
            return new Mapper(config);
        }
        public List<EmployeeView> ViewEmployees()
        {

            Mapper mapper = GetEmployeeViewMapper();
            List<Employee> employees = employeeService.GetEmployees();
            List<Role> roles = roleService.GetAllRoles();

            List<EmployeeView> employeesToView = employees.Join(roles, emp => emp.RoleId, role => role.Id, (employee, role) =>
            {
                EmployeeView employeeToView = mapper.Map<Employee, EmployeeView>(employee);
                employeeToView = mapper.Map(role,employeeToView);
                return employeeToView;
            }).ToList();
            return employeesToView;
        }

        public EmployeeView? ViewEmployee(string empId)
        {
            Mapper mapper = GetEmployeeViewMapper();

            Employee? employee = employeeService.GetEmployeeById(empId);

            EmployeeView? employeeToView = new EmployeeView();
            if (employee == null)
            {
                return null;
            }
            else
            {
                Role role = roleService.GetRoleById(employee.RoleId!);
                if (role == null)
                {
                    return null;
                }
                else
                {
                    employeeToView = mapper.Map<Employee, EmployeeView>(employee);
                    employeeToView = mapper.Map(role, employeeToView);
                }
            }
            return employeeToView;
            
        }

        public Employee? GetEmployeeById(string id)
        {
            return employeeService.GetEmployeeById(id);
        }

        public Employee? AddEmployee(Employee employee)
        {
            return employeeService.AddEmployee(employee);
        }

        public Employee? EditEmployee(Employee employee)
        {
            return employeeService.UpdateEmployee(employee);
        }

        public Employee? DeleteEmployee(string empId)
        {
            return employeeService.DeleteEmployee(empId);
        }

    }
}
