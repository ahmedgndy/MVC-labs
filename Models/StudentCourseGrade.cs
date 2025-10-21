using NuGet.Protocol;
using System.ComponentModel.DataAnnotations;

namespace MVc.Models;

public class StudentCourseGrade
{
  
    public Student Student { get; set; }
    public int StudentId { get; set; } 

    public Course Course { get; set; }
    public Guid CourseId { get; set; }
    public float  Grade { get; set; }
}
