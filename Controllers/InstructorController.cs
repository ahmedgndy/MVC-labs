using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVc.Context;
using MVc.Filters;
using MVc.Models;

namespace MVc.Controllers
{
   

    
        //[RequireRole("Admin")] // remove if you don't want demo auth
        public class InstructorController : Controller
        {
            private readonly SchoolContext _context;
            public InstructorController(SchoolContext context) => _context = context;

            // GET: Instructor
            public async Task<IActionResult> Index()
            {
                var instructors = await _context.Instructors
                    .Include(i => i.OfficeAssignment)
                    .Include(i => i.CourseInstructors).ThenInclude(ci => ci.Course)
                    .ToListAsync();
                return View(instructors);
            }

            // GET: Details
            public async Task<IActionResult> Details(int id)
            {
                var instructor = await _context.Instructors
                    .Include(i => i.OfficeAssignment)
                    .Include(i => i.CourseInstructors).ThenInclude(ci => ci.Course)
                    .FirstOrDefaultAsync(i => i.Id == id);
                if (instructor == null) return NotFound();
                return View(instructor);
            }

            // GET: Create
            [HttpGet]
            public IActionResult Create()
            {
                ViewBag.Courses = _context.Courses.OrderBy(c => c.Name).ToList();
                return View();
            }

            // POST: Create
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(Instructor instructor, int[]? selectedCourses, string? officeLocation)
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Courses = _context.Courses.OrderBy(c => c.Name).ToList();
                    return View(instructor);
                }

                _context.Instructors.Add(instructor);
                await _context.SaveChangesAsync();

                if (!string.IsNullOrWhiteSpace(officeLocation))
                    _context.OfficeAssignments.Add(new OfficeAssignment { InstructorId = instructor.Id, Location = officeLocation });

                if (selectedCourses != null && selectedCourses.Length > 0)
                {
                    foreach (var cid in selectedCourses)
                        _context.CourseInstructors.Add(new CourseInstructor { InstructorId = instructor.Id, CourseId = cid });
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // GET: Edit
            [HttpGet]
            public async Task<IActionResult> Edit(int id)
            {
                var instructor = await _context.Instructors
                    .Include(i => i.OfficeAssignment)
                    .Include(i => i.CourseInstructors)
                    .FirstOrDefaultAsync(i => i.Id == id);
                if (instructor == null) return NotFound();

                ViewBag.Courses = _context.Courses.OrderBy(c => c.Name).ToList();
                ViewBag.SelectedCourseIds = instructor.CourseInstructors.Select(ci => ci.CourseId).ToArray();
                ViewBag.OfficeLocation = instructor.OfficeAssignment?.Location ?? string.Empty;
                return View(instructor);
            }

            // POST: Edit
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, Instructor instructor, int[]? selectedCourses, string? officeLocation)
            {
                if (id != instructor.Id) return BadRequest();

                if (!ModelState.IsValid)
                {
                    ViewBag.Courses = _context.Courses.OrderBy(c => c.Name).ToList();
                    ViewBag.SelectedCourseIds = selectedCourses ?? new int[0];
                    ViewBag.OfficeLocation = officeLocation ?? string.Empty;
                    return View(instructor);
                }

                var existing = await _context.Instructors
                    .Include(i => i.OfficeAssignment)
                    .Include(i => i.CourseInstructors)
                    .FirstOrDefaultAsync(i => i.Id == id);
                if (existing == null) return NotFound();

                existing.Name = instructor.Name;
                existing.HireDate = instructor.HireDate;

                // Office assignment
                if (string.IsNullOrWhiteSpace(officeLocation))
                {
                    if (existing.OfficeAssignment != null) _context.OfficeAssignments.Remove(existing.OfficeAssignment);
                }
                else
                {
                    if (existing.OfficeAssignment == null) _context.OfficeAssignments.Add(new OfficeAssignment { InstructorId = id, Location = officeLocation });
                    else existing.OfficeAssignment.Location = officeLocation;
                }

                // Sync course assignments
                selectedCourses = selectedCourses ?? new int[0];
                var existingCourseIds = existing.CourseInstructors.Select(ci => ci.CourseId).ToList();

                var toRemove = existing.CourseInstructors.Where(ci => !selectedCourses.Contains(ci.CourseId)).ToList();
                if (toRemove.Any()) _context.CourseInstructors.RemoveRange(toRemove);

                var toAdd = selectedCourses.Where(cid => !existingCourseIds.Contains(cid)).ToList();
                foreach (var cid in toAdd) _context.CourseInstructors.Add(new CourseInstructor { InstructorId = id, CourseId = cid });

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // GET: Delete
            [HttpGet]
            public async Task<IActionResult> Delete(int id)
            {
                var instructor = await _context.Instructors
                    .Include(i => i.OfficeAssignment)
                    .Include(i => i.CourseInstructors).ThenInclude(ci => ci.Course)
                    .FirstOrDefaultAsync(i => i.Id == id);
                if (instructor == null) return NotFound();
                return View(instructor);
            }

            // POST: DeleteConfirmed
            [HttpPost, ActionName("DeleteConfirmed")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var instructor = await _context.Instructors
                    .Include(i => i.OfficeAssignment)
                    .Include(i => i.CourseInstructors)
                    .FirstOrDefaultAsync(i => i.Id == id);
                if (instructor == null) return NotFound();

                if (instructor.OfficeAssignment != null) _context.OfficeAssignments.Remove(instructor.OfficeAssignment);
                if (instructor.CourseInstructors.Any()) _context.CourseInstructors.RemoveRange(instructor.CourseInstructors);

                _context.Instructors.Remove(instructor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }
    }

