using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Psinder.Api.Model
{
    public enum Sex
    {
        Samiec, Samica
    }
    public enum BreedType
    {
        Pies, Kot, Inne
    }
    public class Pet
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
        public Shelter Shelter { get; set; } = null!;
    }
}
