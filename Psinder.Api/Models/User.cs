namespace Psinder.Api.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string? Name { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? EmailConfirmed { get; set; }
        public string? PasswordConfirmed { get; set; }
        public string? PhoneNumberConfirmed { get; set; }
    }
}
