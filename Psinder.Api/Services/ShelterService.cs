using Psinder.Api.Models;
using System.Collections.Concurrent;

namespace Psinder.Api.Services
{
    public class ShelterService : IShelterService
    {
        private static ConcurrentDictionary<string, Shelter>? sheltersDictionary;
        public Task<Shelter?> ReadShelter(string id)
        {
            id = id.ToUpper();
            if (sheltersDictionary is null) return null!;
            sheltersDictionary.TryGetValue(id, out Shelter? p);
            return Task.FromResult(p);
        }

        public Task<IEnumerable<Shelter>> ReadAll()
        {
            return Task.FromResult(sheltersDictionary is null ? Enumerable.Empty<Shelter>() : sheltersDictionary.Values);
        }
    }
}
