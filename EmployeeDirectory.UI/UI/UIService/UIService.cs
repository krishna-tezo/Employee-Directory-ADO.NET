﻿using EmployeeDirectory.Core;
using EmployeeDirectory.Interfaces;
using EmployeeDirectory.Models;
using EmployeeDirectory.Models.Models;
using EmployeeDirectory.UI.Interfaces;
using EmployeeDirectory.ViewModel;
using System.Globalization;


namespace EmployeeDirectory.UI.UIServices
{
    internal class UIService : IUIService
    {

        private readonly IRoleController roleController;
        private readonly IEmployeeController employeeController;
        private readonly IProjectController projectController;
        private readonly IValidator validator;

        public UIService(IEmployeeController employeeController, IRoleController roleController, IValidator validator, IProjectController projectController)
        {
            this.roleController = roleController;
            this.employeeController = employeeController;
            this.projectController = projectController;
            this.validator = validator;
        }

        #region "Employee Service"

        //Add Employee
        public void AddEmployee()
        {
            Console.WriteLine("\n----Welcome to Add Employee Form----\n");
            Employee employee = GetEmployeeDetailsFromConsole(new Employee(), EmployeeFormType.Add);
            employeeController.AddEmployee(employee);
            Console.WriteLine("The employee is added successfully");
        }

        //Edit Employee
        public void EditEmployee()
        {
            Employee? employee;
            Console.WriteLine("\n----Welcome To Edit Employee Form----\n");

            string? empId;
            do
            {
                Console.Write("Input Id of the Employee which you want to Edit or -1 to exit: ");
                empId = Console.ReadLine();
                if (empId!.Equals("-1"))
                {
                    break;
                }
                else if (string.IsNullOrEmpty(empId))
                {
                    Console.WriteLine("Don't leave it blank");
                }
                else
                {
                    employee = employeeController.GetEmployeeById(empId);

                    if (employee == null)
                    {
                        Console.WriteLine($"{empId} does not exist");
                    }
                    else
                    {
                        employee = GetEmployeeDetailsFromConsole(employee, EmployeeFormType.Edit, empId);
                        employeeController.EditEmployee(employee);
                        Console.WriteLine($"Employee with id {empId} is updated successfully");
                        break;
                    }

                }
            }
            while (true);
        }


        //Get Employee Details From Console
        public Employee GetEmployeeDetailsFromConsole(Employee employee, EmployeeFormType formType, string? empId = "")
        {

            Console.WriteLine("----Input Employee Details----");
            ValidationResult result;

            string? firstName;
            do
            {
                Console.Write("Enter First Name:");

                firstName = Console.ReadLine();
                if (string.IsNullOrEmpty(firstName))
                {
                    Console.WriteLine("Don't leave it blank");
                }
                else
                {
                    break;
                }

            }
            while (true);


            string? lastName;
            do
            {
                Console.Write("Enter Last Name:");

                lastName = Console.ReadLine();
                if (string.IsNullOrEmpty(lastName))
                {
                    Console.WriteLine("Don't leave it blank");
                }
                else
                {
                    break;
                }
            }
            while (true);

            string? email;
            do
            {
                Console.Write("Enter email:");

                email = Console.ReadLine();


                if (string.IsNullOrEmpty(email))
                {
                    Console.WriteLine("Don't leave it blank");
                }
                else
                {
                    result = validator.ValidateEmail(email);

                    if (result.IsValid != true)
                    {
                        Console.WriteLine(result.ErrorMessage);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            while (true);

            DateTime dob;
            do
            {
                Console.Write("Enter Dob (mm/dd/yyyy):");
                if (DateTime.TryParseExact(Console.ReadLine(), "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dob))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Date Format");

                }
            }
            while (true);

            string? mobileNumber;
            do
            {
                Console.Write("Enter Mobile No.:");

                mobileNumber = Console.ReadLine();


                if (string.IsNullOrEmpty(mobileNumber))
                {
                    Console.WriteLine("Don't leave it blank");
                }
                else
                {
                    result = validator.ValidateMobileNumber(mobileNumber);

                    if (result.IsValid != true)
                    {
                        Console.WriteLine(result.ErrorMessage);
                    }
                    else
                    {
                        break;
                    }
                }

            }
            while (true);

            DateTime joinDate;
            do
            {
                Console.Write("Enter Join Date (mm/dd/yyyy):");

                if (DateTime.TryParseExact(Console.ReadLine(), "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out joinDate))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Date Format");
                }
            }
            while (true);

            string? roleId = GetRoleOptions();
            Tuple<string, string> projectDetails = GetProjectOptions();



            if (formType == EmployeeFormType.Add)
            {
                empId = employeeController.GetNewEmployeeId(firstName, lastName);
            }

            employee.Id = empId;
            employee.FirstName = firstName;
            employee.LastName = lastName;
            employee.Email = email;
            employee.DOB = dob;
            employee.MobileNumber = mobileNumber;
            employee.JoinDate = joinDate;
            employee.RoleId = roleId;
            employee.ProjectId = projectDetails.Item1;
            employee.IsDeleted = false;


            return employee;
        }

        //Get Role
        public string GetRoleOptions()
        {
            string? inputKey;
            int number = 1;
            List<Tuple<string, string, string>> options = new List<Tuple<string, string, string>>();
            Console.WriteLine("\n\n----Available Roles----\n");
            options = roleController.GetRoleNames();

            Dictionary<string, string> optionsMap = [];
            options.ForEach(option =>
            {
                optionsMap.Add(number.ToString(), option.Item1);
                Console.WriteLine(number + ". " + option.Item2 + " - "+ option.Item3);
                number++;
            });

            Console.Write("\nChoose Option:");
            inputKey = Console.ReadLine();
            if (inputKey == null || !optionsMap.ContainsKey(inputKey))
            {
                Console.WriteLine("Please Enter a valid option");
                GetRoleOptions();
            }
            return optionsMap[inputKey!];
        }

        // Get Project Options
        public Tuple<string, string> GetProjectOptions()
        {

            string? inputKey;
            int number = 1;
            List<Tuple<string, string, string>> options = new List<Tuple<string, string, string>>();
            Console.WriteLine("\n\n----Available Projects----\n");
            options = projectController.GetProjectNames();

            Dictionary<string, Tuple<string, string>> optionsMap = [];
            options.ForEach(option =>
            {
                optionsMap.Add(number.ToString(), new Tuple<string, string>(option.Item1, option.Item2));
                Console.WriteLine(number + ". " + option.Item2);
                number++;
            });

            Console.Write("\nChoose Option:");
            inputKey = Console.ReadLine();
            if (inputKey == null || !optionsMap.ContainsKey(inputKey))
            {
                Console.WriteLine("Please Enter a valid option");
                GetRoleOptions();
            }
            return optionsMap[inputKey!];


        }
        //Get Employee Role Details From Roles Data
        public string GetDepartmentsOption()
        {
            string? inputKey;
            int number = 1;
            List<string> options = new List<string>();
            Console.WriteLine("\n\n----Available Departments----\n");
            options = roleController.GetAllDepartments();

            Dictionary<string, string> optionsMap = [];

            options.ForEach(option =>
            {
                optionsMap.Add(number.ToString(), option);
                Console.WriteLine(number + ". " + option);
                number++;
            });
            inputKey = Console.ReadLine();
            if (inputKey == null || !optionsMap.ContainsKey(inputKey))
            {
                Console.WriteLine("Please Enter a valid option");
            }
            return optionsMap[inputKey!];
        }


        //View Employees in Console
        public void ViewEmployees()
        {
            List<EmployeeView> employeesToView = employeeController.ViewEmployees();

            if (employeesToView is null || employeesToView.Count == 0)
                Console.WriteLine("No Employees To Show");
            else
                this.ShowEmployeesDataInTabularFormat(employeesToView);
        }

        //View Single Employee
        public void ViewEmployee()
        {

            List<EmployeeView>? employeesToView = [];

            while (true)
            {
                Console.WriteLine("Enter the emp Id to fetch the employee or -1 to exit:");
                string? empId = Console.ReadLine();
                EmployeeView? employee = new();
                if (empId!.Equals("-1"))
                {
                    break;
                }
                else if (empId != null)
                {
                    employee = employeeController.ViewEmployee(empId);

                    if (employee == null)
                    {
                        Console.WriteLine("The Employee Is Not Found");
                    }
                    else
                    {
                        employeesToView.Add(employee);
                        ShowEmployeesDataInTabularFormat(employeesToView);
                        break;

                    }
                }
            }

        }

        //Delete Employee
        public void DeleteEmployee()
        {
            while (true)
            {
                Console.WriteLine("Enter Employee Id You want to delete OR -1 to exit");
                string empId = Console.ReadLine() ?? string.Empty;
                if (empId.Equals(""))
                {
                    Console.WriteLine("Don't leave blank");
                }
                else if (empId != "-1")
                {
                    int status = employeeController.DeleteEmployee(empId);
                    if (status <= 0)
                        Console.WriteLine("Employee was not found");
                    else
                    {
                        Console.WriteLine("The Employee is Removed");
                    }
                }
                else
                {
                    break;
                }
            }
        }

        //Representation of Data in Tabular Format
        public void ShowEmployeesDataInTabularFormat(List<EmployeeView> employees)
        {
            Console.WriteLine("\nEmployee List");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            string headers = String.Format("|{0,10}|{1,20}|{2,30}|{3,20}|{4,20}|{5,20}|{6,20}|{7,20}|", "EmpId", "Name", "Role", "Department", "Location", "Join Date", "Manager Name", "Project Name");
            Console.WriteLine(headers);
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            employees.ForEach((emp) =>
            {
                string empData = String.Format("|{0,10}|{1,20}|{2,30}|{3,20}|{4,20}|{5,20}|{6,20}|{7,20}|",
                        emp.Id, emp.Name, emp.Role, emp.Department, emp.Location, emp.JoinDate.ToString("MM/dd/yyyy"), emp.ManagerName, emp.ProjectName);
                Console.WriteLine(empData);
            });

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
        }
        #endregion



        #region "Roles Service"

        //Get New Role Details From Console
        public void AddRole()
        {

            Console.WriteLine("\n----Welcome to Add Role Menu----\n");
            Console.WriteLine("\n----Input Role Details----\n");
            string? department = GetDepartmentsOption();

            string? roleName;
            string? location;
            string? description;
            string roleId;
            do
            {
                Console.Write("Enter Role Name: ");
                roleName = Console.ReadLine();

                Console.Write("Enter Location: ");
                location = Console.ReadLine();

                Console.Write("Enter Description:");
                description = Console.ReadLine();

                if (string.IsNullOrEmpty(roleName) || string.IsNullOrEmpty(location) || string.IsNullOrEmpty(description))
                {
                    Console.WriteLine("Don't leave any field");
                }
                else if (roleController.DoesRoleExists(roleName, location))
                {
                    Console.WriteLine("This role already exists");
                }
                else
                {
                    roleId = roleController.GenerateRoleId();
                    break;
                }
                
            } while (true);

            Role role = new()
            {
                Id = roleId,
                Name = roleName,
                Location = location,
                Department = department,
                Description = description
            };


            if (roleController.Add(role) > 0)
            {
                Console.WriteLine("New Role has been added");
            }
        }
        public bool DoesRoleExist(string roleId)
        {
            List<Role> roles = roleController.ViewRoles();
            return roles.Any(role => role.Id == roleId);
        }

        public void ViewAllRoles()
        {
            List<Role> roles = roleController.ViewRoles();

            ShowRolesDataInTabularFormat(roles);
        }
        public void ShowRolesDataInTabularFormat(List<Role> roles)
        {
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");
            string headers = String.Format("|{0,30}|{1,30}|{2,20}|{3,50}|", "RoleName", "Department", "Location", "Description");
            Console.WriteLine(headers);
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");

            roles.ForEach(role =>
            {
                string roleData = String.Format("|{0,30}|{1,30}|{2,20}|{3,50}|", role.Name, role.Department, role.Location, role.Description);
                Console.WriteLine(roleData);
            });
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");
        }
        #endregion
    }
}