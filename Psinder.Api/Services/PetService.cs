using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Psinder.Api.Data;
using Psinder.Api.Models;
using System.Collections.Concurrent;

namespace Psinder.Api.Services;

public class PetService : IPetService
{
    private readonly PsinderContext _context;
    private readonly IMapper _mapper;
    public PetService(PsinderContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<PetModel> GetPet(int id)
    {    
        var pet = await _context.Pets.Where(p => p.PetId == id).FirstOrDefaultAsync();
        return _mapper.Map<PetModel>(pet);
    }

    public async Task<List<PetModel>> GetAllPets()
    {
        var pets = await _context.Pets.ToListAsync();
        return _mapper.Map<List<PetModel>>(pets);
    }
    public async Task<List<PetModel>> SearchPetByName(string name)
    {
        var pets = await _context.Pets.Where(p => p.Name.Contains(name)).ToListAsync();
        return _mapper.Map<List<PetModel>>(pets);
    }

    public async Task<PetModel> AddPet(PetModel model)
    {
        var pet = _mapper.Map<Pet>(model);
        await _context.Pets.AddAsync(pet);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task<PetModel> UpdatePet(PetModel model)
    {
        var pet = _mapper.Map<Pet>(model);
        var result = await _context.Pets.FirstOrDefaultAsync(s => s.PetId == model.PetId);
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

    public async Task<Pet> DeletePet(int id)
    {
        var pet = await _context.Pets.Where(p => p.PetId == id).FirstOrDefaultAsync();
        if (pet != null)
        {
            _context.Pets.Remove(pet);
            await _context.SaveChangesAsync();
            return pet;
        }
        return pet;
    }
}

