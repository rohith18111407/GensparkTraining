using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MigrationProject.Data;
using MigrationProject.Models;

namespace MigrationProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public NewsController(ApplicationDbContext context)
            => _context = context;

        // GET: /api/News
        [HttpGet]
        public async Task<ActionResult<IEnumerable<News>>> GetNews([FromQuery] int page = 1, [FromQuery] int pageSize = 2)
        {
            var items = await _context.News
                .OrderByDescending(n => n.NewsId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return items;
        }

        // GET: /api/News/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<News>> GetNewsItem(int id)
        {
            var item = await _context.News.FindAsync(id);
            if (item == null) return NotFound();
            return item;
        }
    }
}
