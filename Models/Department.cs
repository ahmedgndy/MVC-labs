using Microsoft.Build.ObjectModelRemoting;
using MVc.Enums;
using System.ComponentModel.DataAnnotations;

namespace MVc.Models;

public class Department
{

    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Name { get; set; } = null!;

    // Navigation
    public ICollection<Student> Students { get; set; } = new List<Student>();
}

