using Psinder.Api.Models;
using System.Collections.Concurrent;

namespace Psinder.Api.Services;

public class PetService : IPetService
{
    private static ConcurrentDictionary<string, Pet>? petsDictionary;
    public Task<Pet?> ReadPet(string id)
    {
        id = id.ToUpper();
        if (petsDictionary is null)
        {
            return null!;
        }
        petsDictionary.TryGetValue(id, out Pet? p);
        return Task.FromResult(p);
    }

    public Task<IEnumerable<Pet>> ReadAll()
    {
        return Task.FromResult(petsDictionary is null ? Enumerable.Empty<Pet>() : petsDictionary.Values);
    }
}

