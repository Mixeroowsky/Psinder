﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Psinder.Server.Context;
using Psinder.Server.Dtos;
using Psinder.Server.Entities;

namespace Psinder.Server.Services
{
    public class ShelterService(PsinderDbContext context, IMapper mapper) : IShelterService
    {
        private readonly PsinderDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<ShelterDto> GetShelterById(int id)
        {
            var shelter = await _context.Shelters.Where(p => p.ShelterId == id).FirstOrDefaultAsync();
            return _mapper.Map<ShelterDto>(shelter);
        }

        public async Task<List<ShelterDto>> GetAllShelters()
        {
            var shelters = await _context.Shelters.ToListAsync();
            return _mapper.Map<List<ShelterDto>>(shelters);
        }
        public async Task<ShelterDto> AddShelter(ShelterDto model)
        {
            var shelter = _mapper.Map<Shelter>(model);
            await _context.Shelters.AddAsync(shelter);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<ShelterDto> UpdateShelter(ShelterDto model)
        {
            var shelter = _mapper.Map<Shelter>(model);
            var result = await _context.Shelters.FirstOrDefaultAsync(s => s.ShelterId == model.ShelterId);
            if (result != null)
            {
                result.Name = model.Name;
                result.City = model.City;
                result.PostCode = model.PostCode;
                result.Street = model.Street;
                result.BuldingNumber = model.BuldingNumber;
                result.ApartementNumber = model.ApartementNumber;
                result.PhoneNumber = model.PhoneNumber;
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
