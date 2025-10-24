using Microsoft.AspNetCore.Mvc;
using MVc.Context;
using MVc.Models;
using MVc.ViewModels;
using System.Runtime.CompilerServices;

namespace MVc.Repositories;

public class StudentRepository(SchoolContext context )
{
   public Student GetSudentByID(int id)
    {
        var student = context.Students.FirstOrDefault( s => s.Id == id );
        if (student == null) {
            throw new Exception($"Can not find student with id {id}");
        }
        return student;
    }

   public List<Student> GetSudents()
    {
        var students = context.Students.ToList();
        if (students.Count == 0)
        {
            throw new Exception($"Can not find student with id ");
        }
        Console.WriteLine(students.Count);
        return students;
    }

    
    public void Add(Stduent studentvm)
    {
        var studetn = Stduent.ToModel(studentvm);
        context.Add<Student>(studetn);
        context.SaveChanges();
    }

    public void Edit(Student updatedStudent)
    {
      if (updatedStudent == null) {
            throw new Exception("Student object is null");
      }
      var existing = context.Students.FirstOrDefault(s => updatedStudent.Id == s.Id);
      if (existing is null) return;
        existing.Name = updatedStudent.Name;
        existing.Address = updatedStudent.Address;
        existing.Age = updatedStudent.Age;
        existing.Image = updatedStudent.Image;
        existing.Email = updatedStudent.Email;
        context.SaveChanges();
    }

    public List<Department> GetDepartments() {
       return context.Departments.ToList();
    }
}
