using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Psinder.Api.Models;
using System.Collections.Concurrent;

namespace Psinder.Api.Services;

public class PetService : IPetService
{
    private readonly PsinderContext _context;
    private static ConcurrentDictionary<string, Pet>? petsDictionary;
    public PetService(PsinderContext context)
    {
        _context = context;
    }
    public async Task<Pet?> GetPetById(string id)
    {        
        if (petsDictionary is null)
        {
            return null!;
        }
        petsDictionary.TryGetValue(id, out Pet? p);
        return await Task.FromResult(p);
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

