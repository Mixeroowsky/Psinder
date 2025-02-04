﻿using System.ComponentModel.DataAnnotations;

namespace Psinder.Server.Dtos
{
    public enum Sex
    {
        Male, Female
    }
    public enum BreedType
    {
        Dog, Cat, Other
    }
    public class PetDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter a name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter a description")]
        public string Description { get; set; }
        [Required]
        public Sex Sex { get; set; }
        public int Age { get; set; }
        [Required]
        public BreedType BreedType { get; set; }
        public string PhotoUrl { get; set; }
        public IFormFile ImageFile { get; set; }
        public int ShelterId { get; set; }
    }
}
