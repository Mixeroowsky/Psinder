using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Psinder.Api.Models;
using Psinder.Api.Services;

namespace Psinder.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class PetsController : ControllerBase
{
    private readonly IPetService services;

    public PetsController(IPetService services)
    {
        this.services = services;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Pet>))]
    public async Task<IEnumerable<Pet>> ReadAllPets(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return await services.ReadAll();
        }
        else
        {
            return (await services.ReadAll())
              .Where(p => p.Name == name);
        }
    }


    [HttpGet("{id}", Name = nameof(readPet))] // nazwana ścieżka
    [ProducesResponseType(200, Type = typeof(Pet))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> readPet(string id)
    {
        Pet? p = await services.ReadPet(id);
        if (p == null)
        {
            return NotFound(); // 404 Resource not found
        }
        return Ok(p); // 200 OK with pet in body
    }

    [HttpPost]
    public async Task<IActionResult> createPet(Pet pet)
    {
        return CreatedAtAction("GetUser", new { id = pet.PetId }, pet);
    }

    /*public async Task<IActionResult> DeleteUser(int id)
    {
        if (_context.Users == null)
        {
            return NotFound();
        }
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }*/

}
