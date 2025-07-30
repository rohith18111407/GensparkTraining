using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MigrationProject.Data;
using MigrationProject.DTOs;
using MigrationProject.Models;

namespace MigrationProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactUsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ContactUsController(ApplicationDbContext context)
        => _context = context;

    // POST: /api/ContactUs
    [HttpPost]
    public async Task<ActionResult<ContactU>> SubmitContact([FromBody] ContactU contact)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        contact.Id = 0; // ensure new entry
        _context.ContactUs.Add(contact);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetContact), new { id = contact.Id }, contact);
    }

    // GET: /api/ContactUs/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ContactU>> GetContact(int id)
    {
        var contact = await _context.ContactUs.FindAsync(id);
        if (contact == null) return NotFound();
        return contact;
    }

    // GET: /api/ContactUs
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ContactU>>> GetAll()
        => await _context.ContactUs.ToListAsync();
}