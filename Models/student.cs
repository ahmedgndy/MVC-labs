using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Versioning;

namespace MVc.Models
{
    public class Student
    {
      
        public int Id { get; set; }

        public required string Name { get; set; }
      
        public  int Age { get; set; }
        public string? Address { get; set; }
 
        public string? Image {  get; set; }
        public string? Email {  get; set; }
        public int? StudentCourseGradeId { get; set; }

   
        public List<StudentCourseGrade>? Grads { get; set; }
    }
}
