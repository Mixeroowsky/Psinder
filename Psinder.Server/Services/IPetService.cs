using Psinder.Server.Dtos;
using Psinder.Server.Entities;

namespace Psinder.Server.Services
{
    public interface IPetService
    {
        Task<List<PetDto>> GetAllPets();
        Task<PetDto> GetPet(int id);
        Task<List<PetDto>> SearchPetByName(string name);
        Task<PetDto> AddPet(PetDto model);
        Task<PetDto> UpdatePet(PetDto model);
        Task<Pet> DeletePet(int id);
        Task<ShelterDto> GetPetsShelter(int id);
    }
}
