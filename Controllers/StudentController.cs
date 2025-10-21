using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MVc.Models;
using MVc.Repositories;

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

    public IActionResult AddnewStudent(Student student)
    {
        repo.Add(student);
        return RedirectToAction("Index");
    }
}
