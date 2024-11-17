using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Psinder.Server.AutoMapper;
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
            throw new NotImplementedException();
        }

        public async Task<Pet> DeletePet(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PetDto>> GetAllPets()
        {
            throw new NotImplementedException();
        }

        public async Task<PetDto> GetPet(int id)
        {
            var pet = await _context.Pets.Where(p => p.PetId == id).FirstOrDefaultAsync();
            return _mapper.Map<PetDto>(pet);
        }

        public async Task<List<PetDto>> SearchPetByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<PetDto> UpdatePet(PetDto model)
        {
            throw new NotImplementedException();
        }
    }
}
