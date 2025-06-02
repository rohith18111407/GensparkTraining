using Microsoft.AspNetCore.Mvc;
using MoodJournal.Contexts;
using MoodJournal.DTOs;
using MoodJournal.Models;
using MoodJournal.Services;

namespace MoodJournal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoodJournalController : ControllerBase
    {
        private readonly MoodJournalContext _context;
        private readonly LLMService _llm;

        public MoodJournalController(MoodJournalContext context, LLMService llm)
        {
            _context = context;
            _llm = llm;
        }

        [HttpPost]
        public async Task<ActionResult<MoodEntry>> PostEntry(MoodEntryRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Mood))
                return BadRequest("Mood is required.");

            var entry = new MoodEntry
            {
                Mood = request.Mood,
                Note = request.Note,
                Tip = await _llm.GenerateTipAsync(request.Mood, request.Note),
                Date = DateTime.UtcNow
            };

            await using var transaction = await _context.Database.BeginTransactionAsync();
            _context.MoodEntries.Add(entry);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return CreatedAtAction(nameof(GetEntry), new { id = entry.Id }, entry);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MoodEntry>> GetEntry(int id)
        {
            var entry = await _context.MoodEntries.FindAsync(id);
            if (entry == null) return NotFound();
            return entry;
        }
    }
}