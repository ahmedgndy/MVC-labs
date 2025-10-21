using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MVc.Models;
using MVc.Repositories;
using MVc.ViewModels;

namespace MVc.Controllers;

public class StudentController(StudentRepository repo) : Controller
{
    
    public IActionResult Index()
    {
  
      List<Student> students = repo.GetSudents();
    
      return View(students); //helper methoud
    }

   public IActionResult Add()
    {
        return View();
    }


    [HttpPost]
    public IActionResult AddnewStudent(StudentVM studentvm)
    {
        if (studentvm.Name is not null) { 
            repo.Add(studentvm);
            return RedirectToAction("Index");
        }
        return View("Add",studentvm);
    }
}
