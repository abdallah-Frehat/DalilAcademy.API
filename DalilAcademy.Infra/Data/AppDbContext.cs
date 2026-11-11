using DalilAcademy.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DalilAcademy.Infra.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext (options)

    {
        //DBSets
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Instructor> Instructors => Set<Instructor>();
        public DbSet<Enrollment> Enrollments => Set<Enrollment>();
        public DbSet<User> Users => Set<User>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasIndex(s => s.UserId)
                .IsUnique();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSeeding((context, _) =>
            {
                if (!context.Set<User>().Any(u => u.Role == Roles.Admin))
                {
                    context.Set<User>().Add(new User
                    {
                        Email = "admin@dalil.com",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                        Role = Roles.Admin
                    });
                    context.SaveChanges();
                }

                if (!context.Set<Student>().Any())
                {
                    var studentUser = new User
                    {
                        Email = "abdallah@gmail.com",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("Student@123"),
                        Role = Roles.Student
                    };

                    var student = new Student
                    {
                        FullName = "Abdallah Frehat",
                        Email = "abdallah@gmail.com",
                        PhoneNumber = "0790000000",
                        User = studentUser
                    };

                    context.Set<Student>().Add(student);
                    context.SaveChanges();
                }

                if (!context.Set<Instructor>().Any())
                {
                    var instructor = new Instructor
                    {
                        FullName = "Eng Raghad",
                        Email = "Raghad@gmail.com"
                    };
                    context.Set<Instructor>().Add(instructor);
                    context.SaveChanges();
                }

                if (!context.Set<Course>().Any())
                {
                    var instructor = context.Set<Instructor>().FirstOrDefault();
                    if (instructor != null)
                    {
                        var courses = new List<Course>
                {
                    new Course { Title = "FullStack", Price = 150.00m, InstructorId = instructor.Id },
                    new Course { Title = "QA", Price = 200.00m, InstructorId = instructor.Id }
                };
                        context.Set<Course>().AddRange(courses);
                        context.SaveChanges();
                    }
                }

                if (!context.Set<Enrollment>().Any())
                {
                    var student = context.Set<Student>().FirstOrDefault();
                    var course = context.Set<Course>().FirstOrDefault();

                    if (student != null && course != null)
                    {
                        context.Set<Enrollment>().Add(new Enrollment
                        {
                            StudentId = student.Id,
                            CourseId = course.Id,
                            EnrolledAt = DateTime.UtcNow
                        });
                        context.SaveChanges();
                    }
                }
            });
        }
    }
}