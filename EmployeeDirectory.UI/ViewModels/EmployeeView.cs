﻿    namespace EmployeeDirectory.ViewModel
{
    public class EmployeeView
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Role { get; set; }
        public string? Department { get; set; }
        public string? Location { get; set; }
        public DateTime JoinDate { get; set; }
        public string? ManagerName { get; set; }

        public string? ProjectName { get;set; }


        public override string ToString()
        {
            return "EmpId: " + Id + ", Name: " + Name;
        }
    }
}
