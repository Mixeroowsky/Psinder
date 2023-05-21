using Psinder.Api.Data;
using Psinder.Api.Models;

namespace Psinder.Api.Services;

public interface IPetService
{
    Task<List<PetModel>> GetAllPets();
    Task<PetModel> GetPet(int id);
    Task<List<PetModel>> SearchPetByName(string name);
    Task<PetModel> AddPet(PetModel model);
    Task<PetModel> UpdatePet(PetModel model);
    Task<Pet> DeletePet(int id);
}
