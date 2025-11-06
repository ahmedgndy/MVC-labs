using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MVc.Context;
using MVc.Enums;
using MVc.Models;
using MVc.Services;
using MVc.ViewModels;

namespace MVc.Controllers;
public class StudentController : Controller
{
    private readonly IStudentService _studentService;
    private readonly SchoolContext _context;


    public StudentController(IStudentService studentService, SchoolContext context)
    {
        _studentService = studentService;
        _context = context;
    }


    // GET: Student
    public async Task<IActionResult> Index()
    {
        var students = await _studentService.GetAllAsync();
        return View(students);
    }


    // GET: Student/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var student = await _studentService.GetByIdAsync(id);
        if (student == null) return NotFound();
        return View(student);
    }


    // GET: Student/Create
    public async Task<IActionResult> Create()
    {
        ViewBag.Departments = await _context.Departments.ToListAsync();
        return View("create");
    }


    // POST: Student/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Student student)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Departments = await _context.Departments.ToListAsync();
            return View(student);
        }


        await _studentService.CreateAsync(student);
        return RedirectToAction(nameof(Index));
    }


    
    // GET: Student/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var student = await _studentService.GetByIdAsync(id);
        if (student == null) return NotFound();
        ViewBag.Departments = await _context.Departments.ToListAsync();
        return View(student);
    }

    // POST: Student/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Student student)
    {
        
        Console.WriteLine(student.Id);
        Console.WriteLine("aakdfdjalkfj;asfjas;kfasdf");
        if (id != student.Id) return BadRequest();
        if (!ModelState.IsValid)
        {
            ViewBag.Departments = await _context.Departments.ToListAsync();
            return View(student);
        }


        await _studentService.UpdateAsync(student);
        return RedirectToAction(nameof(Index));
    }


    // GET: Student/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var student = await _studentService.GetByIdAsync(id);
        if (student == null) return NotFound();
        return View(student);
    }


    // POST: Student/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _studentService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
