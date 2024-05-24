namespace EmployeeDirectory.Models.SummaryModels
{
    public class RoleSummary
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public string Department { get; set; }
        public int LocationId { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
    }
}
