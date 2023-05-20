using Psinder.Api.Data;
using Psinder.Api.Models;

namespace Psinder.Api.Services;

public interface IPetService
{
    Task<List<PetModel>> GetAllPets();
    Task<PetModel> GetPetById(int id);
    Task<List<PetModel>> SearchPetByName(string name);
}
