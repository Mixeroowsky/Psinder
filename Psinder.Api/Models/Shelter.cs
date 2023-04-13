using System.ComponentModel.DataAnnotations;

namespace Psinder.Api.Models
{
    public class Shelter
    {
        public int ShelterId { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? Address { get; set; }
        [Phone(ErrorMessage = "Nieprawidłowy numer telefonu")]
        public string? PhoneNumber { get; set; }
        [EmailAddress(ErrorMessage = "Nieprawidłowy adres Email")]
        public string? Email { get; set; }
        public ICollection<Pet>? Pets { get; set; }
    }
}
