using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Psinder.Server.Context;
using Psinder.Server.Dtos;
using Psinder.Server.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Psinder.Server.Services
{
    public class AccountService(IConfiguration configuration, PsinderDbContext context, IPasswordHasher<User> passwordHasher) : IAccountService
    {
        private readonly PsinderDbContext _context = context;
        private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;
        private readonly IConfiguration _configuration = configuration;
        public async Task<string> GenerateJwt(LoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == dto.Email);
            if (user == null)
            {
                return null;
            }
            var password = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (password == PasswordVerificationResult.Failed)
            {
                return null;
            }
            string token = CreateToken(user);
            return token;
        }

        public async Task RegisterUser(RegisterDto dto)
        {
            var newUser = new User()
            {
                Username = dto.Username,
                Email = dto.Email,
            };
            newUser.PasswordHash = _passwordHasher.HashPassword(newUser, dto.Password);
            await _context.AddAsync(newUser);
            await _context.SaveChangesAsync();
        }
        public async Task<User> GetUser(string username, string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email || u.Username == username);
            return user;
        }

        private string CreateToken(User users)
        {
            List<Claim> claims =
            [
                new Claim(ClaimTypes.NameIdentifier, users.Id.ToString()),
                new Claim(ClaimTypes.Name, users.Username),
                new Claim(ClaimTypes.Email, users.Email),

            ];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("ApiSettings:Secret")));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
