using Psinder.Api.Models;

namespace Psinder.Api.Services;

public interface IPetService
{
    Task<IEnumerable<Pet>> ReadAll();
    Task<Pet?> ReadPet(string id);
}
