using EmployeeDirectory.Interfaces;
using EmployeeDirectory.Models;
using EmployeeDirectory.Services;
using System;
using System.Collections.Generic;

namespace EmployeeDirectory.Controllers
{
    public class RoleController : IRoleController
    {
        private readonly IRoleService roleService;

        public RoleController(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        public ServiceResult<Role> ViewRoles()
        {
            return roleService.GetAllRoles();
        }

        public ServiceResult<int> Add(Role role)
        {
            return roleService.Add(role);
        }

        public ServiceResult<bool> DoesRoleExists(string roleName, string location)
        {
            return roleService.DoesRoleExists(roleName, location);
        }

        public ServiceResult<string> GenerateRoleId()
        {
            return roleService.GenerateRoleId();
        }

        public ServiceResult<List<Tuple<string, string, string>>> GetRoleNames()
        {
            return roleService.GetRoleNames();
        }

        public ServiceResult<List<string>> GetAllDepartments()
        {
            return roleService.GetAllDepartments();
        }

        public ServiceResult<List<string>> GetAllRoleNamesByDepartment(string department)
        {
            return roleService.GetAllRoleNamesByDepartment(department);
        }

        public ServiceResult<List<string>> GetAllLocationByDepartmentAndRoleNames(string roleName)
        {
            return roleService.GetAllLocationByDepartmentAndRoleNames(roleName);
        }
    }
}
