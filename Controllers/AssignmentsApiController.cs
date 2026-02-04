using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Schedulo.Data;
using Schedulo.Models;

namespace Schedulo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssignmentsApiController : ControllerBase
{
    private readonly ScheduloDbContext _context;

    public AssignmentsApiController(ScheduloDbContext context)
    {
        _context = context;
    }

    // GET: api/assignments
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Assignment>>> GetAssignments()
    {
        return await _context.Assignments
            .Include(a => a.Course)
            .OrderBy(a => a.DueDate)
            .ToListAsync();
    }

    // GET: api/assignments/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Assignment>> GetAssignment(int id)
    {
        var assignment = await _context.Assignments
            .Include(a => a.Course)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (assignment == null)
            return NotFound();

        return assignment;
    }

    // POST: api/assignments
    [HttpPost]
    public async Task<ActionResult<Assignment>> CreateAssignment(Assignment assignment)
    {
        // Ensure course exists
        var courseExists = await _context.Courses.AnyAsync(c => c.Id == assignment.CourseId);
        if (!courseExists)
            return BadRequest("Invalid CourseId");

        _context.Assignments.Add(assignment);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAssignment), new { id = assignment.Id }, assignment);
    }

    // PUT: api/assignments/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAssignment(int id, Assignment updated)
    {
        if (id != updated.Id)
            return BadRequest();

        _context.Entry(updated).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Assignments.Any(a => a.Id == id))
                return NotFound();

            throw;
        }

        return NoContent();
    }

    // DELETE: api/assignments/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAssignment(int id)
    {
        var assignment = await _context.Assignments.FindAsync(id);
        if (assignment == null)
            return NotFound();

        _context.Assignments.Remove(assignment);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

