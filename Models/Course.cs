using Microsoft.Build.ObjectModelRemoting;

namespace MVc.Models;

public class Course
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public string topic { get; set; }
    public int degree { get; set; }
   public float minDegree {  get; set; }

    public List<Instructor> instructors { get; set; }

    public List<Course> Courses { get; set; }
}
