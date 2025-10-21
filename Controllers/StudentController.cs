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

    public IActionResult AddnewStudent(StudentVM studentvm)
    {
        repo.Add(studentvm);
        return RedirectToAction("Index");
    }
}
