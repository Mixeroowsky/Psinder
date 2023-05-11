using Microsoft.EntityFrameworkCore;
using Psinder.Api.Models;
using System.Drawing;

namespace Psinder.Api.Repositories
{
    public class PetRepository
    {
        private readonly PsinderContext _context;

        public PetRepository(PsinderContext context)
        {
            _context = context;
        }

        public async Task<List<Pet>> GetAllPets()
        {
            return await _context.Pets.Select(pet => new Pet()
            {
                Name = pet.Name,
                Sex = pet.Sex,
                Description = pet.Description,
                Age = pet.Age,
                BreedType = pet.BreedType,
                PhotoUrl = pet.PhotoUrl,
                ShelterId = pet.ShelterId,
                Shelter = pet.Shelter

            }).ToListAsync();
        }
        public async Task<Pet> GetPetById(int id)
        {
            return await _context.Pets.Where(p => p.PetId == id).Select(pet => new Pet()
            {
                Name = pet.Name,
                Sex = pet.Sex,
                Description = pet.Description,                
                Age = pet.Age,
                BreedType = pet.BreedType,
                PhotoUrl = pet.PhotoUrl,
                ShelterId = pet.ShelterId,
                Shelter = pet.Shelter

            }).FirstOrDefaultAsync();
        }

        public async Task<List<Pet>> SearchPetByName (string name)
        {
            return await _context.Pets.Where(p => p.Name.Contains(name)).ToListAsync();
        }
    }
}
