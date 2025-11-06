using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVc.Context;
using MVc.Models;
using MVc.Services;
using MVc.ViewModels;
using NuGet.Versioning;
using System.Runtime.CompilerServices;

namespace MVc.Repositories;
public class StudentService : IStudentService
{
    private readonly SchoolContext _context;


    public StudentService(SchoolContext context)
    {
        _context = context;
    }


    public async Task<List<Student>> GetAllAsync()
    {
        return await _context.Students.Include(s => s.Department).ToListAsync();
    }


    public async Task<Student> GetByIdAsync(int id)
    {
        return await _context.Students
        .Include(s => s.Department)
        .Include(s => s.Enrollments)
        .FirstOrDefaultAsync(s => s.Id == id);
    }


    public async Task CreateAsync(Student student)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
    }


    public async Task UpdateAsync(Student studentDto)
    {
        var existing = await _context.Students
                                        .Include(s => s.Attendances) // if you need the collection
                                        .Include(s => s.Enrollments)   // if you need the collection    
                                        .FirstOrDefaultAsync(s => s.Id == studentDto.Id);

        if (existing == null) return; // or throw

        // Map only the properties you want to update
        existing.Name = studentDto.Name;
        existing.Email = studentDto.Email;
        existing.DateOfBirth = studentDto.DateOfBirth;
        existing.DepartmentId = studentDto.DepartmentId;

        // Carefully update navigation collections instead of replacing them:
        // sync attendances individually (add/remove/update) rather than existing.Attendances = studentDto.Attendances

        await _context.SaveChangesAsync();
    }


    public async Task DeleteAsync(int id)
    {
        var s = await _context.Students.FindAsync(id);
        if (s != null)
        {
            _context.Students.Remove(s);
            await _context.SaveChangesAsync();
        }
    }
}
