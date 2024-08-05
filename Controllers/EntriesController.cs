using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sozluk42.Data;
using Sozluk42.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Sozluk42.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class EntriesController : ControllerBase
    {
        private readonly SozlukContext _context;
        private readonly ILogger<EntriesController> _logger;

        public EntriesController(SozlukContext context, ILogger<EntriesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Entry>>> GetEntries()
        {
            _logger.LogInformation("GetEntries method called");
            try
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

                if (entries == null || !entries.Any())
                {
                    _logger.LogWarning("No entries found");
                    return NotFound("No entries found");
                }

                _logger.LogInformation("Entries retrieved successfully");
                return Ok(entries);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting entries");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{entryId}")]
        public async Task<IActionResult> GetEntryDetails(int entryId)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting entry details");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}/comments")]
        public async Task<IActionResult> GetEntryComments(int id)
        {
            try
            {
                var comments = await _context.Comments
                                            .Where(c => c.EntryId == id)
                                            .Select(c => new
                                            {
                                                c.CommentId,
                                                c.Content,
                                                AuthorUsername = c.User.Username
                                            })
                                            .ToListAsync();

                if (comments == null || !comments.Any())
                {
                    return NotFound();
                }

                return Ok(comments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting entry comments");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [Authorize] // Kullanıcının kimlik doğrulaması yapılmış olmalı
        public async Task<IActionResult> CreateEntry(CreateEntryModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                _logger.LogInformation($"UserId claim value: {userIdClaim}"); // Kullanıcı kimliğini loglayın

                // Eğer userIdClaim bir int değilse, doğru değeri almanız gerekir
                if (!int.TryParse(userIdClaim, out int userId))
                {
                    _logger.LogError($"Invalid user ID format. Received value: {userIdClaim}");
                    return StatusCode(500, $"Invalid user ID format. Received value: {userIdClaim}");
                }

                var entry = new Entry
                {
                    Content = model.Content,
                    TitleId = model.TitleId,
                    UserId = userId // Kullanıcı kimliğini burada ayarlayın
                };

                _context.Entries.Add(entry);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetEntryDetails), new { entryId = entry.EntryId }, entry);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating entry");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
