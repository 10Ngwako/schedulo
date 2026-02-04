using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Schedulo.Data;
using Schedulo.Models;

namespace Schedulo.Pages.Assignments;

public class IndexModel : PageModel
{
    private readonly ScheduloDbContext _context;

    public IndexModel(ScheduloDbContext context)
    {
        _context = context;
    }

    public List<Assignment> Assignments { get; set; } = [];
    public List<Course> Courses { get; set; } = [];

    public async Task OnGetAsync()
    {
        Assignments = await _context.Assignments
            .Include(a => a.Course)
            .OrderBy(a => a.DueDate)
            .ToListAsync();

        Courses = await _context.Courses.ToListAsync();
    }
}
