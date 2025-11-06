

using System.ComponentModel.DataAnnotations;

namespace MVc.Models;

public class Instructor
{
    public int Id { get; set; }


    [Required]
    [StringLength(100)]
    public string Name { get; set; }


    [DataType(DataType.Date)]
    public DateTime HireDate { get; set; }


    public OfficeAssignment? OfficeAssignment { get; set; }
    public ICollection<CourseInstructor>? CourseInstructors { get; set; }
}

