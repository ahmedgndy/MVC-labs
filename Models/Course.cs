using Microsoft.Build.ObjectModelRemoting;
using System.ComponentModel.DataAnnotations;

namespace MVc.Models;

public class Course
{
    
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }

    public string topic { get; set; }
    public int Degree { get; set; }
    public float MinDegree {  get; set; }

    public List<StudentCourseGrade> studentCourseGrades { get; set; }

}
