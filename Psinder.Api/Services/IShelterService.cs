using Psinder.Api.Models;

namespace Psinder.Api.Services
{
    public interface IShelterService
    {
        Task<IEnumerable<Shelter>> ReadAll();
        Task<Shelter?> ReadShelter(string id);
    }
}
