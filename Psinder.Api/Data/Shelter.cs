﻿using System.ComponentModel.DataAnnotations;

namespace Psinder.Api.Data
{
    public class Shelter
    {
        public int ShelterId { get; set; }
        public string? Name { get; set; }
        public string? City { get; set; }
        public string? PostCode { get; set; }
        public string? Street { get; set; }
        public int BuldingNumber { get; set; }
        public int AppartementNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public ICollection<Pet>? Pets { get; set; }
    }
}
