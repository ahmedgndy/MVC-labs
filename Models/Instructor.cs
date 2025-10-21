

using System.ComponentModel.DataAnnotations;

namespace MVc.Models;

public class Instructor
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; }
    public int Age { get; set; }
    public decimal Salary { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }


    public DateTime HireDate { get; set; } 

    public List<Course> Courses { get; set; }
}
