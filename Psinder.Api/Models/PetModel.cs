using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Psinder.Api.Models
{
    public enum Sex
    {
        Samiec, Samica
    }
    public enum BreedType
    {
        Pies, Kot, Inne
    }
    public class PetModel
    {
        public int PetId { get; set; }
        [Required(ErrorMessage = "Podaj imię")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Wprowadź opis")]
        public string? Description { get; set; }
        [Required]
        public Sex Sex { get; set; }
        public int Age { get; set; }
        [Required]
        public BreedType BreedType { get; set; }
        public string? PhotoUrl { get; set; }
        public int ShelterId { get; set; }
        public string Shelter { get; set; } = null!;
    }
}
