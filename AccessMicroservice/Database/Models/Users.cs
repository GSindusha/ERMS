namespace AccessMicroservice.Database.Models
{
    public class Users
    {
        public int Id { get; set; } 
        public string Name { get; set; } = default!;
        public string Gender { get; set; } = default!;
        public string Age { get; set; } = default!;
        public string Salary { get; set; } = default!;
        public string Phone { get; set; } = default!;

        public string EmailId { get; set; } = default!;
        //public string Type { get; set; } = default!;
        public byte[] PasswordHash { get; set; } = default!;
        public byte[] PasswordSalt { get; set; } = default!;


    }
}
