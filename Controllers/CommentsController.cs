using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sozluk42.Data;
using Sozluk42.Models;
using System.Collections.Generic;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetComments()
        {
            return await _context.Comments.Include(c => c.User).Include(c => c.Entry).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(int id)
        {
            var comment = await _context.Comments.Include(c => c.User).Include(c => c.Entry).FirstOrDefaultAsync(c => c.CommentId == id);

            if (comment == null)
            {
                return NotFound();
            }

            return comment;
        }

        [HttpPost]
        public async Task<ActionResult<Comment>> PostComment(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetComment), new { id = comment.CommentId }, comment);
        }
    }
}
