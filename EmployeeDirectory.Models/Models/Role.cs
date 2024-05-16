namespace EmployeeDirectory.Models
{
    public class Role
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Department { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public override string ToString()
        {
            return "RoleId: " + Id + ", Name: " + Name;
        }
    }
}
