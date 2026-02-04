namespace Schedulo.Models;

public class Course
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Instructor { get; set; } = string.Empty;
    public string Semester { get; set; } = string.Empty;

    public List<Assignment> Assignments { get; set; } = [];
}
