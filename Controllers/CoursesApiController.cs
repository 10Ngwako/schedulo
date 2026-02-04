using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Schedulo.Data;
using Schedulo.Models;

namespace Schedulo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesApiController : ControllerBase
{
    private readonly ScheduloDbContext _context;

    public CoursesApiController(ScheduloDbContext context)
    {
        _context = context;
    }

    // GET: api/courses
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
    {
        return await _context.Courses.ToListAsync();
    }

    // GET: api/courses/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Course>> GetCourse(int id)
    {
        var course = await _context.Courses.FindAsync(id);

        if (course == null)
            return NotFound();

        return course;
    }

    // POST: api/courses
    [HttpPost]
    public async Task<ActionResult<Course>> CreateCourse(Course course)
    {
        _context.Courses.Add(course);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, course);
    }

    // DELETE: api/courses/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse(int id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course == null)
            return NotFound();

        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
