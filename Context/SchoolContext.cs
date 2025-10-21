using Microsoft.EntityFrameworkCore;
using MVc.Models;

namespace MVc.Context
{
    public class SchoolContext : DbContext
    {


        public DbSet<Student> Students { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<StudentCourseGrade> StudentCourseGrades { get; set; }
        public DbSet<CourseInstructorHours> CourseInstructorHours { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=SchoolDB;Trusted_Connection=True;Encrypt=False;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourseGrade>()
                .HasKey(scg => new { scg.StudentId, scg.CourseId }); // composite key
                                                                  

        }
    }
}


