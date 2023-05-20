using System.ComponentModel.DataAnnotations;

namespace Psinder.Api.Models
{
    public class ShelterModel
    {
        public int ShelterId { get; set; }
        public string? Name { get; set; }
        public string? City { get; set; }
        [RegularExpression("^[0 - 9]{2}-[0-9]{3}")]
        public string? PostCode { get; set; }
        public string? Street { get; set; }
        public int BuldingNumber { get; set; }
        public int AppartementNumber { get; set; }
        [Phone(ErrorMessage = "Nieprawidłowy numer telefonu")]
        public string? PhoneNumber { get; set; }
        [EmailAddress(ErrorMessage = "Nieprawidłowy adres Email")]
        public string? Email { get; set; }
        public ICollection<PetModel>? Pets { get; set; } = new List<PetModel>();
    }
}
