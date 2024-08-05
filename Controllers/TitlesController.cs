using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sozluk42.Data;
using Sozluk42.Models;

namespace Sozluk42.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TitlesController : ControllerBase
    {
        private readonly SozlukContext _context;

        public TitlesController(SozlukContext context)
        {
            _context = context;
        }

        [HttpGet("{id}/entries")]
        public async Task<IActionResult> GetEntries(int id)
        {
            var title = await _context.Titles
                .Include(t => t.Entries)
                .ThenInclude(e => e.User)
                .FirstOrDefaultAsync(t => t.TitleId == id);

            if (title == null)
            {
                return NotFound();
            }

            var entries = title.Entries.Select(e => new 
            {
                e.EntryId,
                e.Content,
                e.TitleId, 
                AuthorUsername = e.User.Username
            }).ToList();

            return Ok(new
            {
                TitleId = title.TitleId,
                Name = title.Name,
                Entries = entries
            });
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
