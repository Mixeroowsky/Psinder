using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Psinder.Api.Data
{    
    public class Pet
    {
        public int PetId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Sex { get; set; }
        public int Age { get; set; }
        public int BreedType { get; set; }
        public string? PhotoUrl { get; set; }
        public DateTime? CreatedOn { get; set; } = DateTime.Now;
        public DateTime? UpdatedOn { get; set; } = DateTime.Now;
        public int ShelterId { get; set; }
        public Shelter Shelter { get; set; } = null!;
    }
}
