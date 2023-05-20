using Psinder.Api.Models;

namespace Psinder.Api.Services
{
    public interface IShelterService
    {
        Task<List<ShelterModel>> GetAllShelters();
        Task<ShelterModel> GetShelterById(int id);
        Task<int> AddShelter(ShelterModel model);
    }
}
