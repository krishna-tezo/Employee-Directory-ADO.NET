﻿using EmployeeDirectory.UI.Interfaces;
using EmployeeDirectory.Models;
using EmployeeDirectory.ViewModel;
using EmployeeDirectory.Interfaces;
using AutoMapper;
using EmployeeDirectory.Data.Data.Services;
using EmployeeDirectory.Models.Models;
using System.Data;
namespace EmployeeDirectory.UI.Controllers
{
    public class EmployeeController : IEmployeeController
    {
        IEmployeeService employeeService;
        IRoleService roleService;
        IProjectDataService projectDataService;

        public EmployeeController(IEmployeeService employeeService, IRoleService roleService,IProjectDataService projectDataService)
        {
            this.employeeService = employeeService;
            this.roleService = roleService;
            this.projectDataService = projectDataService;
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
            List<Employee> employees = employeeService.GetEmployees();
            List<Role> roles = roleService.GetAllRoles();
            List<Project> projects = projectDataService.GetProjects();

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
