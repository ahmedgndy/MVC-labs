using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVc.Context;
using MVc.Models;

namespace MVc.Controllers
{

 
        public class CourseController : Controller
        {
            private readonly SchoolContext _context;
            public CourseController(SchoolContext context) => _context = context;

            // GET: Course
            public async Task<IActionResult> Index()
            {
                var courses = await _context.Courses
                    .Include(c => c.CourseInstructors).ThenInclude(ci => ci.Instructor)
                    .ToListAsync();
                return View(courses);
            }

            // GET: Course/Details/5
            public async Task<IActionResult> Details(int id)
            {
                var course = await _context.Courses
                    .Include(c => c.CourseInstructors).ThenInclude(ci => ci.Instructor)
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (course == null) return NotFound();
                return View(course);
            }

            // GET: Course/Create
            public IActionResult Create()
            {
                
                ViewBag.Instructors = _context.Instructors.OrderBy(i => i.Name).ToList();
                
                return View();
            }

            // POST: Course/Create
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(Course course, int[]? selectedInstructors)
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Instructors = _context.Instructors.OrderBy(i => i.Name).ToList();
                    return View(course);
                }

                _context.Courses.Add(course);
                await _context.SaveChangesAsync(); // get course.Id

                if (selectedInstructors != null && selectedInstructors.Length > 0)
                {
                    foreach (var iid in selectedInstructors)
                        _context.CourseInstructors.Add(new CourseInstructor { CourseId = course.Id, InstructorId = iid });

                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            // GET: Course/Edit/5
            public async Task<IActionResult> Edit(int id)
            {
                var course = await _context.Courses
                    .Include(c => c.CourseInstructors)
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (course == null) return NotFound();

                ViewBag.Instructors = _context.Instructors.OrderBy(i => i.Name).ToList();
                ViewBag.SelectedInstructorIds = course.CourseInstructors.Select(ci => ci.InstructorId).ToArray();
                return View(course);
            }

            // POST: Course/Edit/5
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, Course course, int[]? selectedInstructors)
            {
                if (id != course.Id) return BadRequest();

                if (!ModelState.IsValid)
                {
                    ViewBag.Instructors = _context.Instructors.OrderBy(i => i.Name).ToList();
                    ViewBag.SelectedInstructorIds = selectedInstructors ?? new int[0];
                    return View(course);
                }

                var existing = await _context.Courses
                    .Include(c => c.CourseInstructors)
                    .FirstOrDefaultAsync(c => c.Id == id);
                if (existing == null) return NotFound();

                existing.Name = course.Name;
                existing.Credits = course.Credits;

                // sync instructors
                selectedInstructors = selectedInstructors ?? new int[0];
                var existingInstrIds = existing.CourseInstructors.Select(ci => ci.InstructorId).ToList();

                // remove unchecked
                var toRemove = existing.CourseInstructors.Where(ci => !selectedInstructors.Contains(ci.InstructorId)).ToList();
                if (toRemove.Any()) _context.CourseInstructors.RemoveRange(toRemove);

                // add newly checked
                var toAdd = selectedInstructors.Where(iid => !existingInstrIds.Contains(iid)).ToList();
                foreach (var iid in toAdd)
                    _context.CourseInstructors.Add(new CourseInstructor { CourseId = id, InstructorId = iid });

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // GET: Course/Delete/5
            public async Task<IActionResult> Delete(int id)
            {
                var course = await _context.Courses
                    .Include(c => c.CourseInstructors).ThenInclude(ci => ci.Instructor)
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (course == null) return NotFound();
                return View(course);
            }

            // POST: Course/DeleteConfirmed/5
            [HttpPost, ActionName("DeleteConfirmed")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var course = await _context.Courses
                    .Include(c => c.CourseInstructors)
                    .FirstOrDefaultAsync(c => c.Id == id);
                if (course == null) return NotFound();

                if (course.CourseInstructors.Any()) _context.CourseInstructors.RemoveRange(course.CourseInstructors);
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
        }
    }

