using MVc.Context;
using MVc.Models;
using MVc.ViewModels;

namespace MVc.Repositories;

public class CourseRepository(SchoolContext context)
{
  
        public Course GetCourseByID(Guid id)
        {
            var course = context.Courses.FirstOrDefault(s => s.Id == id);
            if (course == null)
            {
                throw new Exception($"Can not find student with id {id}");
            }
            return course;
        }

        public List<Course> GetAllCourses()
        {
            var courses = context.Courses.ToList();
            if (courses.Count == 0)
            {
                throw new Exception($"Can not find student with id ");
            }
          
            return courses;
        }


       
        public List<Department> GetDepartments()
        {
            return context.Departments.ToList();
        }
    }


