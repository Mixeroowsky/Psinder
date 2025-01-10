using Psinder.Server.Dtos;
using Psinder.Server.Entities;

namespace Psinder.Server.Services
{
    public interface IShelterService
    {
        Task<List<ShelterDto>> GetAllShelters();
        Task<ShelterDto> GetShelterById(int id);
        Task<ShelterDto> GetShelterByName(string name);
        Task<ShelterDto> GetShelterByUser(int id);
        Task<ShelterDto> AddShelter(ShelterDto model);
        Task<ShelterDto> UpdateShelter(ShelterDto model);
        Task<Shelter> DeleteShelter(int id);
    }
}
