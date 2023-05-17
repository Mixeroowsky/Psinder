using Psinder.Api.Data;

namespace Psinder.Api.Services;

public interface IPetService
{
    Task<List<Pet>> GetAllPets();
    Task<Pet?> GetPetById(int id);
    Task<List<Pet>> SearchPetByName(string name);
}
