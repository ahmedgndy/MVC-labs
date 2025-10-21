namespace MVc.Models;

public class CourseInstructorHours
{
    public int Id { get; set; }

    public Guid InstructorId { get; set; }
    public Instructor Instructor { get; set; }

    public Guid CourseId { get; set; }
    public Course Course { get; set; }

    public int Hours { get; set; }
}
