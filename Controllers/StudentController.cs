using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MVc.Models;
using MVc.Repositories;

namespace MVc.Controllers;
[Route("student")]
public class StudentController(StudentRepository repo) : Controller
{
    
    public IActionResult Index()
    {
      var id = new Guid("7f4d6f43-5b82-4c68-9df1-9f2b5b07d4e0") ;
       var student = repo.GetSudentByID(id);

        return View(student); //helper methoud
    }





}
