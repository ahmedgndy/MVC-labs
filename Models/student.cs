using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Versioning;

namespace MVc.Models
{
   
        public class Student
        {
            [Key]
            public int Id { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Full name ")]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters")] //server side only 
        [MaxLength(15, ErrorMessage = "Name cannot exceed 15 characters")]
        public string Name { get; set; } = null!;

            [Required, EmailAddress(ErrorMessage = "Invalid email address"), StringLength(200)]
      

        public string Email { get; set; } = null!;

        [DataType(DataType.Date)]
            [Display(Name = "Date of Birth")]
            public DateTime DateOfBirth { get; set; }

            // FK to Department
            [Display(Name = "Department")]  
             public int DepartmentId { get; set; }
            public Department? Department { get; set; }

            // Navigation
            public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
            public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
        }
    }

