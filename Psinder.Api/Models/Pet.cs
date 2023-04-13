using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Psinder.Api.Models
{
    public enum Sex
    {
        male, female
    }
    public enum BreedType
    {
        dog, cat, other
    }
    public class Pet
    {
        public int PetId { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Sex Sex { get; set; }
        public int Age { get; set; }
        public BreedType BreedType { get;set; }
        public string? PhotoUrl { get; set; }        
        public Shelter? Shelter { get; set; }
    }
}
