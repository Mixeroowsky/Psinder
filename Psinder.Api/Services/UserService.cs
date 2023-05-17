using Psinder.Api.Data;
using System.Collections.Concurrent;

namespace Psinder.Api.Services
{
    public class UserService : IUserService
    {
        private static ConcurrentDictionary<string, User>? UsersDictionary;
        public Task<User?> ReadUser(string id)
        {
            id = id.ToUpper();
            if (UsersDictionary is null) return null!;
            UsersDictionary.TryGetValue(id, out User? p);
            return Task.FromResult(p);
        }

        public Task<IEnumerable<User>> ReadAll()
        {
            return Task.FromResult(UsersDictionary is null ? Enumerable.Empty<User>() : UsersDictionary.Values);
        }
    }
}
