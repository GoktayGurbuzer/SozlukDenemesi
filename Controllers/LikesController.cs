using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sozluk42.Data;
using Sozluk42.Models;
using System.Threading.Tasks;

namespace Sozluk42.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        private readonly SozlukContext _context;

        public LikesController(SozlukContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> LikeEntry([FromBody] Like like)
        {
            if (like == null)
            {
                return BadRequest();
            }

            _context.Likes.Add(like);
            await _context.SaveChangesAsync();

            return Ok(like);
        }

        [HttpDelete("{entryId}/{userId}")]
        [Authorize]
        public async Task<IActionResult> UnlikeEntry(int entryId, int userId)
        {
            var like = await _context.Likes.FirstOrDefaultAsync(l => l.EntryId == entryId && l.UserId == userId);
            if (like == null)
            {
                return NotFound();
            }

            _context.Likes.Remove(like);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{entryId}")]
        public async Task<IActionResult> GetLikes(int entryId)
        {
            var likes = await _context.Likes
                .Include(l => l.User)
                .Where(l => l.EntryId == entryId)
                .ToListAsync();

            if (likes == null)
            {
                return NotFound();
            }

            var likeList = likes.Select(l => new
            {
                l.LikeId,
                l.UserId,
                l.User.Username
            });

            return Ok(likeList);
        }
    }
}
