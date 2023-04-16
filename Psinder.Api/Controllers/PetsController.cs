using Microsoft.AspNetCore.Mvc;
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
        return Ok(p); // 200 OK with customer in body
    }
}
