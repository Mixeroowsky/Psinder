using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Psinder.Api.Data;
using System.Collections.Concurrent;

namespace Psinder.Api.Services;

public class PetService : IPetService
{
    private readonly PsinderContext _context;
    public PetService(PsinderContext context)
    {
        _context = context;
    }
    public async Task<Pet?> GetPetById(int id)
    {        
        return await _context.Pets.Where(p => p.PetId== id).FirstOrDefaultAsync();
    }

    public async Task<List<Pet>> GetAllPets()
    {
        var pets = await _context.Pets.ToListAsync();
        return pets;
    }
    public async Task<List<Pet>> SearchPetByName(string name)
    {
        return await _context.Pets.Where(p => p.Name.Contains(name)).ToListAsync();
    }
}

