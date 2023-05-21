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

        public ShelterService(PsinderContext context, IMapper mapper)
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
        public async Task<ShelterModel> AddShelter(ShelterModel model)
        {
            var shelter = _mapper.Map<Shelter>(model);
            await _context.Shelters.AddAsync(shelter);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<ShelterModel> UpdateShelter(ShelterModel model)
        {
            var shelter = _mapper.Map<Shelter>(model);
            var result = await _context.Shelters.FirstOrDefaultAsync(s => s.ShelterId == model.ShelterId);
            if (result != null)
            {
                result.Name= model.Name;
                result.City = model.City;
                result.PostCode= model.PostCode;
                result.Street = model.Street;
                result.BuldingNumber= model.BuldingNumber;
                result.AppartementNumber= model.AppartementNumber;
                result.PhoneNumber= model.PhoneNumber;                
                result.Email = model.Email;
                await _context.SaveChangesAsync();
                return model;
            }
            return model;
        }

        public async Task<Shelter> DeleteShelter(int id)
        {
            var shelter = await _context.Shelters.FirstOrDefaultAsync(s => s.ShelterId == id);
            if (shelter != null)
            {
                _context.Shelters.Remove(shelter);
                await _context.SaveChangesAsync();
                return shelter;
            }
            return shelter;            
        }
    }
}
