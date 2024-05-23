using EmployeeDirectory.Data.Data.Services;
using EmployeeDirectory.Models;
namespace EmployeeDirectory.Services
{

    public class EmployeeService : IEmployeeService
    {
        private IEmployeeDataService employeeDataService;
        
        public EmployeeService(IEmployeeDataService employeeDataService)
        {
            this.employeeDataService = employeeDataService;
        }

        public ServiceResult<Employee> GetEmployees()
        {
            List<Employee> employees = [];
            try
            {
                employees = employeeDataService.GetEmployees();
                employees = employees.Where(emp => emp.IsDeleted == false).ToList();
                if(employees.Count == 0)
                {
                    return ServiceResult<Employee>.Fail("No Employee To Show");
                }
                return ServiceResult<Employee>.Success(employees);
            }
            catch (Exception ex)
            {
                return ServiceResult<Employee>.Fail("Database Issue:"+ex.Message);
            }
        }

        public ServiceResult<Employee> GetEmployeeById(string id)
        {
            try
            {
                Employee employee = employeeDataService.GetEmployeeById(id);
                if(employee == null)
                {
                    return ServiceResult<Employee>.Fail($"Employee Id {id} doesn't exist");
                }
                return ServiceResult<Employee>.Success(employee);
            }
            catch(Exception ex)
            {
                return ServiceResult<Employee>.Fail("Database Issue:"+ex.Message);
            }
        }
        
        public ServiceResult<int> AddEmployee(Employee employee)
        {
            try
            {
                int rowsAffected = employeeDataService.AddEmployee(employee);
                if(rowsAffected == 0)
                {
                    return ServiceResult<int>.Fail("Database Connectivity Issue");
                }
                return ServiceResult<int>.Success(rowsAffected,$"{rowsAffected} data has been inserted");
            }
            catch(Exception ex)
            {
                return ServiceResult<int>.Fail("Database Issue:"+ex.Message);
            }
        }

        public ServiceResult<int> DeleteEmployee(string empId)
        {
            try
            {
                int rowsAffected = employeeDataService.DeleteEmployee(empId);
                if (rowsAffected == 0)
                {
                    return ServiceResult<int>.Fail("Id doesn't exist");
                }
                return ServiceResult<int>.Success(rowsAffected,$"{rowsAffected} employee has been deleted");
            }
            catch(Exception ex)
            {
                return ServiceResult<int>.Fail("Database Issue:"+ex.Message);
            }
        }

        public ServiceResult<int> UpdateEmployee(Employee newEmployee)
        {
            try {
                List<Employee> employees = GetEmployees().DataList;
                Employee? existingEmployee = employees.Find((emp) => emp.Id == newEmployee.Id);
                if(existingEmployee != null)
                {
                    int rowsAffected = employeeDataService.UpdateEmployee(newEmployee);
                    return ServiceResult<int>.Success(rowsAffected,$"{rowsAffected} employee has been updated");
                }
                else
                {
                    return ServiceResult<int>.Fail("Id Doesn't exist");
                }
            }
            catch(Exception ex)
            {
                return ServiceResult<int>.Fail("Database Issue:"+ex.Message);
            }
        }
        public ServiceResult<string> GenerateNewId(string firstName, string lastName)
        {
            string lastEmpId = employeeDataService.GetLastEmployeeId();
            string prefix = "TEZ";
            string numericPart = lastEmpId.Substring(prefix.Length);

            if (int.TryParse(numericPart, out int numericId))
            {
                int newNumericId = numericId + 1;
                string newEmpId = prefix + newNumericId.ToString("D5");
                return ServiceResult<string>.Success(newEmpId);
            }
            else
            {
                return ServiceResult<string>.Fail("Invalid Employee Id Format");
            }
        }
    }
}