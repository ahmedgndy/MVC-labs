using MVc.Models;
using System.ComponentModel.DataAnnotations;

namespace MVc.ViewModels;

public class Stduent
{

    [Display(Name = "Full Name")]
    public  string Name { get; set; }

    public  int Age { get; set; }
    public string? Address { get; set; }
    public string? Image { get; set; }
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }

    public List<CourseCheckBox>? Courses { get; set; }

    public static Student ToModel(Stduent vm)
    {
        return new Student
        {
            Name = vm.Name,
            Address = vm.Address,
            Image = vm.Image,
            Email = vm.Email
        };
    }
}
