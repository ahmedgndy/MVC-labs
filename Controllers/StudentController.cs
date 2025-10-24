using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MVc.Models;
using MVc.Repositories;
using MVc.ViewModels;

namespace MVc.Controllers;

public class StudentController(StudentRepository Srepo , CourseRepository Courserepo) : Controller
{
    
    public IActionResult Index()
    {
  
      List<Student> students = Srepo.GetSudents();
      
      return View(students); //helper methoud
    }

   public IActionResult Add()
    
    {
        var Courses = Courserepo.GetAllCourses();
        var model = new Stduent();
         List<CourseCheckBox> checkedcoursed = [] ;
        foreach(var course in Courses)
        {
            var checkBox = new CourseCheckBox();
            checkBox.Id = course.Id;
            checkBox.Name = course.Name;
            checkedcoursed.Add(checkBox);
        }
        model.Courses = checkedcoursed;
        return View(model);
    }


    [HttpPost]
    public IActionResult AddnewStudent(Stduent studentvm)
    {
        if (studentvm.Name is not null) {
            Srepo.Add(studentvm);
            return RedirectToAction("Index");
        }
        return View("Add",studentvm);
    }

    [HttpGet]
    public IActionResult Edit(int id )
    {
        var student = Srepo.GetSudentByID(id); //tracked
        if(student is null)
        {
               return NotFound();
        } 
        return View(student);
    }

    [HttpPost]
    public IActionResult Edit(Student student)
    {
         Srepo.Edit(student);
      
        return RedirectToAction("Index");
    }
}
