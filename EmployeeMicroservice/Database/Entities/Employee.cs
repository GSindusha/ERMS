namespace EmployeeMicroservice.Database.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Gender { get; set; } = default!;
        public string Age { get; set; } = default!;
        public string Salary { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string EmailId { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Type { get; set; } = default!;
    }
}
