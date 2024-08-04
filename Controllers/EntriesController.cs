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
                    e.EntryId,
                    e.Content,
                    TitleName = e.Title.Name,
                    AuthorUsername = e.User.Username
                })
                .ToListAsync();

            return Ok(entries);
        }
    }
}
