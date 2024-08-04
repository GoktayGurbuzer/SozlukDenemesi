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
    public class TitlesController : ControllerBase
    {
        private readonly SozlukContext _context;

        public TitlesController(SozlukContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Title>>> GetTitles()
        {
            return await _context.Titles.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Title>> GetTitle(int id)
        {
            var title = await _context.Titles
                .Include(t => t.Entries)
                .ThenInclude(e => e.User)
                .FirstOrDefaultAsync(t => t.TitleId == id);

            if (title == null)
            {
                return NotFound();
            }

            return title;
        }

        [HttpPost]
        public async Task<ActionResult<Title>> CreateTitle([FromBody] Title title)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Titles.Add(title);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTitle), new { id = title.TitleId }, title);
        }
    }
}
