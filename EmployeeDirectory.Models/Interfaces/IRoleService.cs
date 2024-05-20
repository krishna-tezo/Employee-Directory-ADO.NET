﻿using EmployeeDirectory.Models;

namespace EmployeeDirectory.Interfaces
{
    public interface IRoleService
    {
        int Add(Role role);
        bool DoesRoleExists(string  roleName, string location);
        string GenerateRoleId();
        Role GetRoleById(string id);
        List<Tuple<string, string, string>> GetRoleNames();
        List<string> GetAllDepartments();
        List<string> GetAllLocationByDepartmentAndRoleNames(string roleName);
        List<string> GetAllRoleNamesByDepartment(string department);

        List<Role> GetAllRoles();

    }
}