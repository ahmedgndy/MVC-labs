using Microsoft.Build.ObjectModelRemoting;
using MVc.Enums;

namespace MVc.Models;

public class Department
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Manager { get; set; }


    public Branch Branche { get; set; }

    public List<Student> Students { get; set; }

    public List<Instructor> Instructors { get; set; } 
}
