using Microsoft.Build.ObjectModelRemoting;
using MVc.Enums;
using System.ComponentModel.DataAnnotations;

namespace MVc.Models;

public class Department
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public String Manager { get; set; }

    public Branch Branche { get; set; }

    public List<Student> Students { get; set; }

    public List<Instructor> Instructors { get; set; } 
}
