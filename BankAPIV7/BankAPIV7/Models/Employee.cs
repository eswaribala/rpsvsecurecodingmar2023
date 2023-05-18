namespace BankAPIV7.Models
{
    public class Employee
    {
        public long SSN { get; set; }
        public string? Name { get; set; }
        public DateTime DOB { get; set; }   
        public string? EmployeeType { get; set; }
        public long Salary { get; set; }
    }
}
