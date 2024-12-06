namespace Psinder.Server.Entities
{
    public class Shelter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string Street { get; set; }
        public int BuildingNumber { get; set; }
        public int ApartmentNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public ICollection<Pet> Pets { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
