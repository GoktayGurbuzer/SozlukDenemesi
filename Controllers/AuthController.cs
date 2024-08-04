using Microsoft.AspNetCore.Mvc;
using Sozluk42.Data;
using Sozluk42.Models;
using Sozluk42.Services;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Sozluk42.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly SozlukContext _context;
        private readonly TokenService _tokenService;

        public AuthController(SozlukContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == login.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(login.Password, user.Password))
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }

            var token = _tokenService.GenerateToken(user);
            return Ok(new { token });
        }
    }
}
