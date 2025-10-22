using MVc.Models;

namespace MVc.ViewModels;

public class StudentVM
{
    public string Name { get; set; }

    public int Age { get; set; }
    public string? Address { get; set; }
    public string? Image { get; set; }
    public string? Email { get; set; }

    public List<CourseCheckBox> Courses { get; set; }
    public static Student ToModel(StudentVM vm)
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
