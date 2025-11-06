using Microsoft.EntityFrameworkCore;
using MVc.Models;

namespace MVc.Context
{

    public class SchoolContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<CourseInstructor> CourseInstructors { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=SchoolDB;Trusted_Connection=True;Encrypt=False;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // CourseInstructor composite key
            modelBuilder.Entity<CourseInstructor>()
            .HasKey(ci => new { ci.CourseId, ci.InstructorId });


            modelBuilder.Entity<CourseInstructor>()
            .HasOne(ci => ci.Course)
            .WithMany(c => c.CourseInstructors)
            .HasForeignKey(ci => ci.CourseId);


            modelBuilder.Entity<CourseInstructor>()
            .HasOne(ci => ci.Instructor)
            .WithMany(i => i.CourseInstructors)
            .HasForeignKey(ci => ci.InstructorId);


            // One-to-one Instructor - OfficeAssignment
            modelBuilder.Entity<Instructor>()
            .HasOne(i => i.OfficeAssignment)
            .WithOne(o => o.Instructor)
            .HasForeignKey<OfficeAssignment>(o => o.InstructorId);


            // Seed some data (optional)
            modelBuilder.Entity<Department>().HasData(
            new Department { Id = 1, Name = "Computer Science" },
            new Department { Id = 2, Name = "Mathematics" }
            );
       
        }
    }
}


