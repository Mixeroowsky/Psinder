using Psinder.Api.Data;
using Psinder.Api.Models;

namespace Psinder.Api.Services
{
    public interface IShelterService
    {
        Task<List<ShelterModel>> GetAllShelters();
        Task<ShelterModel> GetShelterById(int id);
        Task<ShelterModel> AddShelter(ShelterModel model);
        Task<ShelterModel> UpdateShelter(ShelterModel model);
        Task<Shelter> DeleteShelter(int id);
    }
}
