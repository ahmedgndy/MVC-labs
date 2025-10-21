using System.Runtime.Versioning;

namespace MVc.Models
{
    public class Student
    {
        public string Name { get; set; }
        public Guid Id { get; set; } 
        public  int Age { get; set; }
        public string? Address { get; set; }
        public string? Image {  get; set; }
        public string? Email {  get; set; }

        public List<Student> Course { get; set; }
    }
}
