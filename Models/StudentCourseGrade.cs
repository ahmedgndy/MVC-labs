using NuGet.Protocol;

namespace MVc.Models;

public class StudentCourseGrade
{
    public int Id { get; set; }
    //relations 
    public Student Student { get; set; }
    public Guid StudentId { get; set; } 

    public Course Course { get; set; }
    public Guid CouseId { get; set; }
    public float  Grade { get; set; }
}
