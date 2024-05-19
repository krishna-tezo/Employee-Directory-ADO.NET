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
            string lastEmpId = EmployeeDataService.GetLastEmployeeId();
            string prefix = "TEZ";
            string numericPart = lastEmpId.Substring(prefix.Length);

            if (int.TryParse(numericPart, out int numericId))
            {

                int newNumericId = numericId + 1;

                string newEmpId = prefix + newNumericId.ToString("D5");
                return newEmpId;
            }
            else
            {
                throw new ArgumentException("Invalid employee ID format.");
            }
        }


        public Employee AddEmployee(Employee employee)
        {
            employeeDataService.AddEmployee(employee);
            return employee;
        }

        public int DeleteEmployee(string empId)
        {
            //List<Employee> employees = GetEmployees();
            //Employee? employee = employees.Find(emp => emp.Id == empId);
            //if (employee != null)
            //{
            //    employee.IsDeleted = true;
            //    jsonDataHandler.UpdateDataToJson<Employee>(employees);
            //    employees.Remove(employee);
            //    return employee;
            //}
            //else
            //{
            //    return null;
            //}
            return employeeDataService.DeleteEmployee(empId);

        }

        public int UpdateEmployee(Employee newEmployee)
        {
            List<Employee> employees = GetEmployees();
            Employee? existingEmployee = employees.Find((emp) => emp.Id == newEmployee.Id);

            if (existingEmployee != null)
            {
                existingEmployee = newEmployee;
            }
            return employeeDataService.UpdateEmployee(existingEmployee);
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