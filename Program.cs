// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

Console.WriteLine("Hello, World!");
using (var context = new MyContext())
{
    // Create and save a new Students
    Console.WriteLine("Adding new students");

    var student = new Student
    {
        FirstMidName = "Atyia",
        LastName = "Alam",
        EmailID = "atyia@Alam.com",
        Section = "Afternoon",
        EnrollmentDate = DateTime.Parse(DateTime.Today.ToString())
    };

    context.Students.Add(student);

    var student1 = new Student
    {
        FirstMidName = "Ali",
        LastName = "Ahmed",
        EmailID = "Ali@Ahmad.com",
        Section = "Morning",
        EnrollmentDate = DateTime.Parse(DateTime.Today.ToString())
    };

    context.Students.Add(student1);
    context.SaveChanges();

    // Display all Students from the database
    var students = (from s in context.Students
                    orderby s.FirstMidName
                    select s).ToList<Student>();

    Console.WriteLine("Retrieve all Students from the database:");

    foreach (var stdnt in students)
    {
        string name = stdnt.FirstMidName + " " + stdnt.LastName;
        Console.WriteLine("ID: {0}, Name: {1}, Email:{2}, Section:{3}", stdnt.ID, name, stdnt.EmailID, stdnt.Section);
    }
    Console.WriteLine("Updating information");
    var studentToBeUpdated = context.Students.FirstOrDefault(s => s.ID == 1);

    if (studentToBeUpdated != null)
    {
        studentToBeUpdated.EmailID = "newl@student.com";
        studentToBeUpdated.Section = "Morning";
        context.SaveChanges();
    }
    // Display all Students from the database after updating
    var studentss = (from s in context.Students
                     orderby s.FirstMidName
                     select s).ToList<Student>();

    Console.WriteLine("Retrieve all Students from the database:");

    foreach (var stdnt in studentss)
    {
        string name = stdnt.FirstMidName + " " + stdnt.LastName;
        Console.WriteLine("ID: {0}, Name: {1}, Email:{2}, Section:{3}", stdnt.ID, name, stdnt.EmailID, stdnt.Section);
    }
    Console.WriteLine("Deleting  student");
    var studentToBeDeleted = context.Students.FirstOrDefault(s => s.ID == 2);

    if (studentToBeDeleted != null)
    {
        context.Students.Remove(studentToBeDeleted);
        context.SaveChanges();

    }

    // Display all Students from the database after deleting
    var studentsss = (from s in context.Students
                      orderby s.FirstMidName
                      select s).ToList<Student>();

    Console.WriteLine("Retrieve all Students from the database:");

    foreach (var stdnt in studentsss)
    {
        string name = stdnt.FirstMidName + " " + stdnt.LastName;
        Console.WriteLine("ID: {0}, Name: {1}, Email:{2}, Section:{3}", stdnt.ID, name, stdnt.EmailID, stdnt.Section);
    }

    Console.WriteLine("Press any key to exit...");
    Console.ReadKey();
}
public enum Grade
{
    A, B, C, D, F
}
public class Enrollment
{
    public int EnrollmentID { get; set; }
    public int CourseID { get; set; }
    public int StudentID { get; set; }
    public Grade? Grade { get; set; }

    public virtual Course? Course { get; set; }
    public virtual Student? Student { get; set; }
}

public class Student
{
    public int ID { get; set; }
    public string? LastName { get; set; }
    public string? FirstMidName { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public string? EmailID { get; set; }
    public string? Section { get; set; }

    public virtual ICollection<Enrollment>? Enrollments { get; set; }
}

public class Course
{
    public int CourseID { get; set; }
    public string? Title { get; set; }
    public int Credits { get; set; }

    public virtual ICollection<Enrollment>? Enrollments { get; set; }
}

public class MyContext : DbContext
{
    public virtual DbSet<Course> Courses { get; set; }
    public virtual DbSet<Enrollment> Enrollments { get; set; }
    public virtual DbSet<Student> Students { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
       => options.UseSqlServer(@"Data Source=DESKTOP-K1RP3GB\SQLEXPRESS;Initial Catalog=mydatabase;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False");
}