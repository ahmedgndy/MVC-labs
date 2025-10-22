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

    
    public void Add(StudentVM studentvm)
    {
        var studetn = StudentVM.ToModel(studentvm);
        context.Add<Student>(studetn);
        context.SaveChanges();
    }

    public List<Department> GetDepartments() {
       return context.Departments.ToList();
    }
}
