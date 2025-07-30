using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MigrationProject.Data;
using MigrationProject.DTOs;
using MigrationProject.Models;

namespace MigrationProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsManagementController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public NewsManagementController(ApplicationDbContext context)
            => _context = context;

        // GET: api/NewsManagement
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NewsDto>>> GetAll()
        {
            var newsList = await _context.News.Include(n => n.User).ToListAsync();
            var dtoList = newsList.Select(n => new NewsDto
            {
                NewsId = n.NewsId,
                UserId = n.UserId,
                Title = n.Title,
                ShortDescription = n.ShortDescription,
                Image = n.Image,
                Content = n.Content,
                CreatedDate = n.CreatedDate,
                Status = n.Status
            }).ToList();

            return dtoList;
        }

        // GET: api/NewsManagement/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<NewsDto>> Get(int id)
        {
            var news = await _context.News.FindAsync(id);
            if (news == null) return NotFound();

            var dto = new NewsDto
            {
                NewsId = news.NewsId,
                UserId = news.UserId,
                Title = news.Title,
                ShortDescription = news.ShortDescription,
                Image = news.Image,
                Content = news.Content,
                CreatedDate = news.CreatedDate,
                Status = news.Status
            };

            return dto;
        }

        // POST: api/NewsManagement
        [HttpPost]
        public async Task<ActionResult<NewsDto>> Create([FromBody] NewsDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var news = new News
            {
                UserId = dto.UserId,
                Title = dto.Title,
                ShortDescription = dto.ShortDescription,
                Image = dto.Image,
                Content = dto.Content,
                CreatedDate = dto.CreatedDate ?? DateTime.UtcNow,
                Status = dto.Status
            };

            _context.News.Add(news);
            await _context.SaveChangesAsync();

            dto.NewsId = news.NewsId;
            dto.CreatedDate = news.CreatedDate;

            return CreatedAtAction(nameof(Get), new { id = news.NewsId }, dto);
        }

        // PUT: api/NewsManagement/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] NewsDto dto)
        {
            if (id != dto.NewsId) return BadRequest("ID mismatch");

            var existingNews = await _context.News.FindAsync(id);
            if (existingNews == null) return NotFound();

            existingNews.UserId = dto.UserId;
            existingNews.Title = dto.Title;
            existingNews.ShortDescription = dto.ShortDescription;
            existingNews.Image = dto.Image;
            existingNews.Content = dto.Content;
            existingNews.CreatedDate = dto.CreatedDate ?? existingNews.CreatedDate;
            existingNews.Status = dto.Status;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.News.Any(n => n.NewsId == id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // DELETE: api/NewsManagement/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.News.FindAsync(id);
            if (item == null) return NotFound();

            _context.News.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
