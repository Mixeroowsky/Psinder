using System.ComponentModel.DataAnnotations;

namespace Psinder.Server.Dtos
{
    public class ShelterDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        [RegularExpression("\\d{2}-\\d{3}", ErrorMessage = "Enter a valid postal code in format: 12-123")]
        public string PostCode { get; set; }
        public string Street { get; set; }
        public int BuldingNumber { get; set; }
        public int ApartementNumber { get; set; }
        [Phone(ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email address")]
        public string Email { get; set; }
        public ICollection<PetDto> Pets { get; set; }
    }
}
