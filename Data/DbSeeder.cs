using Schedulo.Models;

namespace Schedulo.Data;

public static class DbSeeder
{
    public static void Seed(ScheduloDbContext context)
    {
        if (context.Courses.Any())
            return; // DB already seeded

        var courses = new List<Course>
        {
            new Course
            {
                Name = "Software Engineering",
                Code = "CS301",
                Instructor = "Dr. Smith",
                Semester = "Spring 2026"
            },
            new Course
            {
                Name = "Database Systems",
                Code = "CS305",
                Instructor = "Prof. Johnson",
                Semester = "Spring 2026"
            }
        };

        context.Courses.AddRange(courses);
        context.SaveChanges();

        var assignments = new List<Assignment>
        {
            new Assignment
            {
                Title = "Project Proposal",
                CourseId = courses[0].Id,
                DueDate = DateTime.Today.AddDays(-2),
                Priority = "High",
                Status = "Pending"
            },
            new Assignment
            {
                Title = "ER Diagram",
                CourseId = courses[1].Id,
                DueDate = DateTime.Today.AddDays(3),
                Priority = "Medium",
                Status = "Pending"
            },
            new Assignment
            {
                Title = "Final Project",
                CourseId = courses[0].Id,
                DueDate = DateTime.Today.AddDays(10),
                Priority = "High",
                Status = "Pending"
            }
        };

        context.Assignments.AddRange(assignments);
        context.SaveChanges();
    }
}
