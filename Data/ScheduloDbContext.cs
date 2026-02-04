using Microsoft.EntityFrameworkCore;
using Schedulo.Models;

namespace Schedulo.Data;

public class ScheduloDbContext : DbContext
{
    public ScheduloDbContext(DbContextOptions<ScheduloDbContext> options)
        : base(options) { }

    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Assignment> Assignments => Set<Assignment>();
}
