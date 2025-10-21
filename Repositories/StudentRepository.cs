using MVc.Context;
using MVc.Models;
using System.Runtime.CompilerServices;

namespace MVc.Repositories
{
    public class StudentRepository(SchoolContext context )
    {
       public Student GetSudentByID(int id)
        {
            var student = context.Students.FirstOrDefault( s => s.Id == id );
            if (student == null) {
                throw new Exception($"Can not find student with id {id}");
            }
            return student;
        }
    
       public List<Student> GetSudents()
        {
            var students = context.Students.ToList();
            if (students.Count == 0)
            {
                throw new Exception($"Can not find student with id ");
            }
            Console.WriteLine(students.Count);
            return students;
        }

     public void Add(Student student)
        {
            context.Add<Student>(student);
            context.SaveChanges();
        }
    }

}
