using Psinder.Server.Dtos;
using Psinder.Server.Entities;

namespace Psinder.Server.Services
{
    public interface IAccountService
    {
        public Task<string> GenerateJwt(LoginDto dto);
        public Task RegisterUser(RegisterDto dto);
        public Task<User> GetUser(string username, string email);
    }
}
