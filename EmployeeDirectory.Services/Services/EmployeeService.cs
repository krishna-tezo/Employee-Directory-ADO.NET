using EmployeeDirectory.Data.Data.Services;
using EmployeeDirectory.DATA;
using EmployeeDirectory.Interfaces;
using EmployeeDirectory.Models;
namespace EmployeeDirectory.Services
{

    public class EmployeeService : IEmployeeService
    {

        
        private IJsonDataHandler jsonDataHandler;
        private IEmployeeDataService employeeDataService;
        
        public EmployeeService(IJsonDataHandler jsonDataHandler,IEmployeeDataService employeeDataService, IProjectDataService projectDataService)
        {
            this.jsonDataHandler = jsonDataHandler;
            this.employeeDataService = employeeDataService;
           
        }
        public string GenerateNewId(string firstName, string lastName)
        {
            List<Employee> employees = this.GetEmployees();
            Random random = new Random();
            string newRoleId = "TZ" + firstName.Substring(0, 1).ToUpper() + lastName.Substring(0, 1).ToUpper() + (random.Next()%10000).ToString("D4");
            if (employees.Exists((emp) => emp.Id == newRoleId))
            {
                GenerateNewId(firstName, lastName);
            }

            return newRoleId;
        }


        public Employee AddEmployee(Employee employee)
        {
            List<Employee> employees = GetEmployees();
            employees.Add(employee);
            jsonDataHandler.UpdateDataToJson<Employee>(employees);
            return employee;
        }

        public Employee? DeleteEmployee(string empId)
        {
            List<Employee> employees = GetEmployees();
            Employee? employee = employees.Find(emp => emp.Id == empId);
            if (employee != null)
            {
                employee.IsDeleted = true;
                jsonDataHandler.UpdateDataToJson<Employee>(employees);
                employees.Remove(employee);
                return employee;
            }
            else
            {
                return null;
            }
        }

        public Employee UpdateEmployee(Employee newEmployee)
        {
            List<Employee> employees = GetEmployees();
            Employee? existingEmployee = employees.Find((emp) => emp.Id == newEmployee.Id);

            if (existingEmployee != null)
            {
                existingEmployee = newEmployee;
            }
            jsonDataHandler.UpdateDataToJson<Employee>(employees);
            return newEmployee;
           
        }

        public List<Employee> GetEmployees()
        {
            List<Employee> employees = [];
            try
            {
                //employees = jsonDataHandler.GetDataFromJson<Employee>();
                employees = employeeDataService.GetEmployees();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            employees = employees.Where(emp => emp.IsDeleted == false).ToList();
            return employees;
        }

        public Employee GetEmployeeById(string id)
        {
            List<Employee> employees = GetEmployees();
            return employees.Find((emp) => emp.Id == id)!;
        }
    }
}