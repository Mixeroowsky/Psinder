using Psinder.Api.Models;
using System.Collections.Concurrent;

namespace Psinder.Api.Services;

public class PetService : IPetService
{
    // dane klientów umieszczane są w wielowątkowym słowniku,
    // co znacznie poprawia szybkość pracy
    private static ConcurrentDictionary
     <string, Pet>? petsDictionary;
    public Task<Pet?> ReadPet(string id)
    {
        // pobierz z pamięci podręcznej - tak jest szybciej
        id = id.ToUpper();
        if (petsDictionary is null) return null!;
        petsDictionary.TryGetValue(id, out Pet? k);
        return Task.FromResult(k);
    }

    public Task<IEnumerable<Pet>> ReadAll()
    {
        // pobierz z pamięci podręcznej - tak jest szybciej
        return Task.FromResult(petsDictionary is null
          ? Enumerable.Empty<Pet>() : petsDictionary.Values);
    }
}

