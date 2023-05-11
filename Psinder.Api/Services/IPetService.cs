using Psinder.Api.Models;

namespace Psinder.Api.Services;

public interface IPetService
{
    Task<List<Pet>> GetAllPets();
    Task<Pet?> GetPetById(string id);
    Task<List<Pet>> SearchPetByName(string name);
}
