//using Microsoft.EntityFrameworkCore;
//using MVc.Context;
//using MVc.Models;
//using MVc.ViewModels;

//namespace MVc.Repositories;

//public class CourseRepository(SchoolContext context)
//{
//    public Course GetCourseByID(Guid id)
//    {
//        var course = context.Courses.FirstOrDefault(s => s.Id == id);
//        if (course == null)
//        {
//            throw new Exception($"Can not find student with id {id}");
//        }

//        return course;
//    }
//    public List<Course> GetCourse()
//    {
//        var courses = context.Courses.ToList();
//        if (courses.Count == 0)
//        {
//            throw new Exception($"Can not find student with id ");
//        }

//        return courses;
//    }


//        public List<Course> GetAllCourses()
//        {
//            var courses = context.Courses.ToList();
//            if (courses.Count == 0)
//            {
//                throw new Exception($"Can not find student with id ");
//            }
          
//            return courses;
//        }


       
//        public List<Department> GetDepartments()
//        {
//            return context.Departments.ToList();
//        }
//    public void Add(Course course)
//    {
//        context.Add<Course>(course);
//        context.SaveChanges();


//    }

//    public void Edit(Course updatedCourse)
//    {
//        if (updatedCourse == null)
//            throw new ArgumentNullException(nameof(updatedCourse));

//        // Find the existing course
//        var existing = context.Courses
//            .Include(c => c.studentCourseGrades) // Include enrolled students
//            .FirstOrDefault(c => c.Id == updatedCourse.Id);

//        if (existing == null) return;

//        // Update basic course properties
//        existing.Name = updatedCourse.Name;
//        existing.topic = updatedCourse.topic;
//        existing.Degree = updatedCourse.Degree;
//        existing.MinDegree = updatedCourse.MinDegree;

       
         

//        context.SaveChanges();
//    }


//}


