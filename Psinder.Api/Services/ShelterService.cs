using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Psinder.Api.Data;
using Psinder.Api.Models;
using System.Collections.Concurrent;

namespace Psinder.Api.Services
{
    public class ShelterService : IShelterService
    {
        private readonly PsinderContext _context;
        private readonly IMapper _mapper;

        ShelterService(PsinderContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ShelterModel> GetShelterById(int id)
        {
            var shelter = await _context.Shelters.Where(p => p.ShelterId == id).FirstOrDefaultAsync();
            return _mapper.Map<ShelterModel>(shelter);
        }

        public async Task<List<ShelterModel>> GetAllShelters()
        {
            var shelters = await _context.Shelters.ToListAsync();
            return _mapper.Map<List<ShelterModel>>(shelters);
        }
        public async Task<int> AddShelter(ShelterModel model)
        {
            var shelter = _mapper.Map<Shelter>(model);
            await _context.Shelters.AddAsync(shelter);
            await _context.SaveChangesAsync();
            return shelter.ShelterId;
        }
    }
}
