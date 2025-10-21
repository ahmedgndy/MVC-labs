using System.ComponentModel.DataAnnotations;

namespace MVc.Models;

public class CourseInstructorHours
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid InstructorId { get; set; }
    public Instructor Instructor { get; set; }

    public Guid CourseId { get; set; }
    public Course Course { get; set; }

    public int Hours { get; set; }
}
