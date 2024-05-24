namespace EmployeeDirectory.Data.SummaryModels
{
    public class EmployeeSummary
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DOB { get; set; }
        public string MobileNumber { get; set; }
        public DateTime JoinDate { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int RoleId { get; set; }
        public string Role { get; set; }
        public int DepartmentId { get; set; }
        public string Department { get; set; }
        public int LocationId { get; set; }
        public string Location { get; set; }
        public int ManagerId { get; set; }
        public string ManagerName { get; set; }
    }

}
