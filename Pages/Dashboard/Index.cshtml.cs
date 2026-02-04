using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Schedulo.Data;
using Schedulo.Models;

namespace Schedulo.Pages.Dashboard;

public class IndexModel : PageModel
{
    private readonly ScheduloDbContext _context;

    public IndexModel(ScheduloDbContext context)
    {
        _context = context;
    }

    public List<Assignment> Overdue { get; set; } = [];
    public List<Assignment> Upcoming { get; set; } = [];

    public async Task OnGetAsync()
    {
        var today = DateTime.Today;
        var nextWeek = today.AddDays(7);

        Overdue = await _context.Assignments
            .Include(a => a.Course)
            .Where(a => a.DueDate < today && a.Status != "Submitted")
            .OrderBy(a => a.DueDate)
            .ToListAsync();

        Upcoming = await _context.Assignments
            .Include(a => a.Course)
            .Where(a => a.DueDate >= today && a.DueDate <= nextWeek)
            .OrderBy(a => a.DueDate)
            .ToListAsync();
    }
}
