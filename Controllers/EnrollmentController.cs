using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVc.Context;
using MVc.Models;

namespace MVc.Controllers
{

        public class EnrollmentController : Controller
        {
            private readonly SchoolContext _context;

            public EnrollmentController(SchoolContext context)
            {
                _context = context;
            }

            // GET: Enrollment
            public async Task<IActionResult> Index()
            {
                var enrollments = await _context.Enrollments
                    .Include(e => e.Student)
                    .Include(e => e.Course)
                    .ToListAsync();
                return View(enrollments);
            }

            // GET: Enrollment/Details/5
            public async Task<IActionResult> Details(int? id)
            {
                if (id == null) return NotFound();

                var enrollment = await _context.Enrollments
                    .Include(e => e.Student)
                    .Include(e => e.Course)
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (enrollment == null) return NotFound();

                return View(enrollment);
            }

            // GET: Enrollment/Create
            public IActionResult Create()
            {
                ViewBag.Students = new SelectList(_context.Students, "Id", "Name");
                ViewBag.Courses = new SelectList(_context.Courses, "Id", "Name");
                return View();
            }

            // POST: Enrollment/Create
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(Enrollment enrollment)
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Students = new SelectList(_context.Students, "Id", "Name", enrollment.StudentId);
                    ViewBag.Courses = new SelectList(_context.Courses, "Id", "Name", enrollment.CourseId);
                    return View(enrollment);
                }

                _context.Enrollments.Add(enrollment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // GET: Enrollment/Edit/5
            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null) return NotFound();

                var enrollment = await _context.Enrollments.FindAsync(id);
                if (enrollment == null) return NotFound();

                ViewBag.Students = new SelectList(_context.Students, "Id", "Name", enrollment.StudentId);
                ViewBag.Courses = new SelectList(_context.Courses, "Id", "Name", enrollment.CourseId);
                return View(enrollment);
            }

            // POST: Enrollment/Edit/5
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, Enrollment enrollment)
            {
                if (id != enrollment.Id) return NotFound();

                if (!ModelState.IsValid)
                {
                    ViewBag.Students = new SelectList(_context.Students, "Id", "Name", enrollment.StudentId);
                    ViewBag.Courses = new SelectList(_context.Courses, "Id", "Name", enrollment.CourseId);
                    return View(enrollment);
                }

                try
                {
                    _context.Update(enrollment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Enrollments.Any(e => e.Id == enrollment.Id))
                        return NotFound();
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            // GET: Enrollment/Delete/5
            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null) return NotFound();

                var enrollment = await _context.Enrollments
                    .Include(e => e.Student)
                    .Include(e => e.Course)
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (enrollment == null) return NotFound();

                return View(enrollment);
            }

            // POST: Enrollment/DeleteConfirmed/5
            [HttpPost, ActionName("DeleteConfirmed")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var enrollment = await _context.Enrollments.FindAsync(id);
                if (enrollment != null)
                {
                    _context.Enrollments.Remove(enrollment);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
        }
    }



