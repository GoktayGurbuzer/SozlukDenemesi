using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sozluk42.Data;
using Sozluk42.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization; // Bu satırı ekleyin

namespace Sozluk42.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Tüm endpoint'ler için kimlik doğrulaması gereksin
    public class UsersController : ControllerBase
    {
        private readonly SozlukContext _context;

        public UsersController(SozlukContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users
                .Include(u => u.Entries)
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User updatedUser)
        {
            if (id != updatedUser.UserId)
            {
                return BadRequest();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.Username = updatedUser.Username;
            user.Email = updatedUser.Email;
            if (!string.IsNullOrEmpty(updatedUser.Password))
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(updatedUser.Password);
            }

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}/password")]
        public async Task<IActionResult> ChangePassword(int id, [FromBody] ChangePasswordRequest request)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

    public class ChangePasswordRequest
    {
        public string NewPassword { get; set; }
    }
}
