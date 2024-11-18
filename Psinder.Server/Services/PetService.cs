using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Psinder.Server.Context;
using Psinder.Server.Dtos;
using Psinder.Server.Entities;

namespace Psinder.Server.Services
{
    public class PetService(PsinderDbContext context, IMapper mapper) : IPetService
    {
        private readonly PsinderDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<PetDto> AddPet(PetDto model)
        {
            var pet = _mapper.Map<Pet>(model);
            await _context.Pets.AddAsync(pet);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Pet> DeletePet(int id)
        {
            var pet = await _context.Pets.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (pet != null)
            {
                _context.Pets.Remove(pet);
                await _context.SaveChangesAsync();
                return pet;
            }
            return pet;
        }

        public async Task<List<PetDto>> GetAllPets()
        {
            var pets = await _context.Pets.ToListAsync();
            return _mapper.Map<List<PetDto>>(pets);
        }

        public async Task<PetDto> GetPet(int id)
        {
            var pet = await _context.Pets.FirstOrDefaultAsync(p => p.Id == id);
            return _mapper.Map<PetDto>(pet);
        }

        public async Task<ShelterDto> GetPetsShelter(int id)
        {
            var shelter = await _context.Shelters.FirstOrDefaultAsync(s => s.Id == id);
            return _mapper.Map<ShelterDto>(shelter);
        }

        public async Task<List<PetDto>> SearchPetByName(string name)
        {
            var pets = await _context.Pets.Where(p => p.Name.Contains(name)).ToListAsync();
            return _mapper.Map<List<PetDto>>(pets);
        }

        public async Task<PetDto> UpdatePet(PetDto model)
        {
            var pet = _mapper.Map<Pet>(model);
            var result = await _context.Pets.FirstOrDefaultAsync(p => p.Id == model.Id);
            if (result != null)
            {
                result.Name = model.Name;
                result.Description = model.Description;
                result.BreedType = (int)model.BreedType;
                result.Sex = (int)model.Sex;
                result.Age = model.Age;
                result.PhotoUrl = model.PhotoUrl;
                await _context.SaveChangesAsync();
                return model;
            }
            return model;
        }
    }
}
