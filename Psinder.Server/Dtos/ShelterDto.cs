using System.ComponentModel.DataAnnotations;

namespace Psinder.Server.Dtos
{
    public class ShelterDto
    {
        public int ShelterId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        [RegularExpression("^[0 - 9]{2}-[0-9]{3}")]
        public string PostCode { get; set; }
        public string Street { get; set; }
        public int BuldingNumber { get; set; }
        public int ApartementNumber { get; set; }
        [Phone(ErrorMessage = "Invalid phone numbe")]
        public string PhoneNumber { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email address")]
        public string Email { get; set; }
        public ICollection<PetDto> Pets { get; set; }
    }
}
