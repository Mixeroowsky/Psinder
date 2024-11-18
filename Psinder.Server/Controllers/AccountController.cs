using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Psinder.Server.Dtos;
using Psinder.Server.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Psinder.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IAccountService accountService) : ControllerBase
    {
        private readonly IAccountService _accountService = accountService;

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody] RegisterDto dto)
        {
            var user = await _accountService.GetUser(dto.Username, dto.Email);
            if (user != null)
            {
                return BadRequest("User already exists");
            }
            await _accountService.RegisterUser(dto);
            return Created("success", dto);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDto dto)
        {
            var token = await _accountService.GenerateJwt(dto);
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(1)
            };

            if (token == null)
            {
                return Unauthorized();
            }
            Response.Cookies.Append("jwtToken", token, cookieOptions);
            return Ok(new
            {
                message = "success",
                token
            });
        }
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            if (Request.Cookies["jwtToken"] != null)
            {
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.UtcNow.AddDays(-1)
                };

                Response.Cookies.Append("jwtToken", "", cookieOptions);
            }
            return Ok(new { message = "Successfully logged out" });
        }
        [HttpGet("auth")]
        [Authorize]
        public IActionResult CheckSession()
        {
            var userName = User.Identity?.Name;
            if (userName != null)
            {
                return Ok(
                new
                {
                    isAuthenticated = true,
                    user = userName
                });
            }
            return Unauthorized();

        }
    }
}
