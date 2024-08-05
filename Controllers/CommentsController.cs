using Microsoft.AspNetCore.Authorization;
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
    public class CommentsController : ControllerBase
    {
        private readonly SozlukContext _context;

        public CommentsController(SozlukContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddComment([FromBody] Comment comment)
        {
            if (comment == null)
            {
                return BadRequest();
            }

            var entry = await _context.Entries.FindAsync(comment.EntryId);
            var user = await _context.Users.FindAsync(comment.UserId);

            if (entry == null || user == null)
            {
                return BadRequest("Entry or User not found.");
            }

            comment.Entry = entry;
            comment.User = user;

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return Ok(comment);
        }


        [HttpGet("{entryId}")]
        public async Task<IActionResult> GetCommentsByEntry(int entryId)
        {
            var comments = await _context.Comments
                .Where(c => c.EntryId == entryId)
                .Include(c => c.User)
                .ToListAsync();

            if (comments == null)
            {
                return NotFound();
            }

            var commentList = comments.Select(c => new
            {
                c.CommentId,
                c.Content,
                AuthorUsername = c.User.Username
            });

            return Ok(commentList);
        }
    }
}
