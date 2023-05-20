using System.ComponentModel.DataAnnotations;

namespace Psinder.Api.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string? Name { get; set; }
        [Display(Name= "Numer telefonu")]
        [Required(ErrorMessage = "Wprowadź numer telefonu")]
        [Phone(ErrorMessage = "Wprowadź poprawny numer telefonu")]
        public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = "Wprowadź adres email")]
        [Display(Name = "Adres email")]
        [EmailAddress(ErrorMessage = "Wprowadź poprawny email")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Wprowadź hasło")]
        [Compare("ConfirmPassword", ErrorMessage = "Hasła się nie zgadzają")]
        [Display(Name = "Hasło")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Please confirm your password")]
        [Display(Name = "Potwierdź hasło")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }
    }
}
