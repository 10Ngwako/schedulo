namespace Schedulo.Models;

public class Assignment
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime DueDate { get; set; }

    public string Status { get; set; } = "Pending";
    public string Priority { get; set; } = "Medium";

    public int CourseId { get; set; }
    public Course? Course { get; set; }
}
