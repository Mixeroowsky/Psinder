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
    public async Task<PetModel> GetPetById(int id)
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
}

