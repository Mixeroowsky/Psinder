using Psinder.Api.Data;

namespace Psinder.Api.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> ReadAll();
        Task<User?> ReadUser(string id);
    }
}
