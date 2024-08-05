using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sozluk42.Data;
using Sozluk42.Models;
using Microsoft.AspNetCore.Cors;

namespace Sozluk42.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class EntriesController : ControllerBase
    {
        private readonly SozlukContext _context;

        public EntriesController(SozlukContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Entry>>> GetEntries()
        {
            var entries = await _context.Entries
                .Include(e => e.Title)
                .Include(e => e.User)
                .Select(e => new
                {
                    EntryId = e.EntryId,
                    e.TitleId,
                    e.Content,
                    TitleName = e.Title.Name,
                    AuthorUsername = e.User.Username
                })
                .ToListAsync();

            return Ok(entries);
        }

        [HttpGet("{entryId}")]
        public async Task<IActionResult> GetEntryDetails(int entryId)
        {
            var entry = await _context.Entries
                .Include(e => e.User)
                .Include(e => e.Comments)
                .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(e => e.EntryId == entryId);

            if (entry == null)
            {
                return NotFound();
            }

            var entryDetails = new 
            {
                entry.EntryId,
                entry.Content,
                entry.TitleId,
                AuthorUsername = entry.User.Username,
                Comments = entry.Comments.Select(c => new 
                {
                    c.CommentId,
                    c.Content,
                    AuthorUsername = c.User.Username
                }).ToList()
            };

            return Ok(entryDetails);
        }

    }
}
