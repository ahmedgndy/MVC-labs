using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVc.Context;
using MVc.Filters;
using MVc.Models;

namespace MVc.Controllers
{
    //[RequireRole("Admin")] // protected by simple authorization filter
    public class DepartmentController : Controller
    {
        private readonly SchoolContext _context;


        public DepartmentController(SchoolContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _context.Departments.ToListAsync());
        }


        public IActionResult Create() => View();


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department dept)
        {
            if (!ModelState.IsValid) return View(dept);
            _context.Departments.Add(dept);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Edit(int id)
        {
            var d = await _context.Departments.FindAsync(id);
            if (d == null) return NotFound();
            return View(d);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Department dept)
        {
            if (id != dept.Id) return BadRequest();
            if (!ModelState.IsValid) return View(dept);

            var existing = await _context.Departments.FindAsync(id);
            if (existing == null) return NotFound();
            existing.Name = dept.Name;

            _context.Departments.Update(existing);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int id)
        {
            var d = await _context.Departments.FindAsync(id);
            if (d == null) return NotFound();
            return View(d);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var d = await _context.Departments.FindAsync(id);
            if (d != null) _context.Departments.Remove(d);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}