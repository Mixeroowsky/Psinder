using Psinder.Api.Models;

namespace Psinder.Api.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> ReadAll();
        Task<User?> ReadUser(string id);
    }
}
