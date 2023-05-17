using Psinder.Api.Data;

namespace Psinder.Api.Services
{
    public interface IShelterService
    {
        Task<IEnumerable<Shelter>> ReadAll();
        Task<Shelter?> ReadShelter(string id);
    }
}
