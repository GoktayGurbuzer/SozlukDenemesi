using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sozluk42.Data;
using Sozluk42.Models;
using System.Linq;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Like>>> GetLikes()
        {
            return await _context.Likes.Include(l => l.User).Include(l => l.Entry).ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> PostLike(Like like)
        {
            var existingLike = await _context.Likes
                .FirstOrDefaultAsync(l => l.EntryId == like.EntryId && l.UserId == like.UserId);

            if (existingLike != null)
            {
                _context.Likes.Remove(existingLike);
                await _context.SaveChangesAsync();
                return NoContent();
            }

            _context.Likes.Add(like);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLikes), new { id = like.LikeId }, like);
        }
    }
}
