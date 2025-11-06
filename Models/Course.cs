using Microsoft.Build.ObjectModelRemoting;
using System.ComponentModel.DataAnnotations;

namespace MVc.Models;

public class Course
{

    public int Id { get; set; }

    [Required, StringLength(150)]
    public string Name { get; set; } = null!;

    [Range(0, 30)]
    public int Credits { get; set; }

    // Navigation
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    public ICollection<CourseInstructor>? CourseInstructors { get; set; }


}
