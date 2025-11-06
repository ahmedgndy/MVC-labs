using Microsoft.CodeAnalysis.CSharp.Syntax;
using MVc.Models;
using System.ComponentModel.DataAnnotations;

namespace MVc.ViewModels;

public class StudentViewModel
{
    public int Id { get; set; }

    [MinLength(3, ErrorMessage = "Name must be at least 3 characters")] //server side only 
    [MaxLength(15, ErrorMessage = "Name cannot exceed 15 characters")]

    [Display(Name = "Full Name")]
    public  string Name { get; set; }

    [Range(21, 60, ErrorMessage = "Age must be between 21 and 60")]
    public int Age { get; set; }
    public string? Address { get; set; }
    public string? Image { get; set; }

    [DataType(DataType.EmailAddress)]

    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string? Email { get; set; }


   

}
