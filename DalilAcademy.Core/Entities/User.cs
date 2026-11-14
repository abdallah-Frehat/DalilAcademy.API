using System;
using System.Collections.Generic;
using System.Text;

namespace DalilAcademy.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public string PasswordHash { get; set; } = string.Empty;// never real password
        public string Role { get; set; } = Roles.Student;
    }
    public static class Roles
    {
        public const string Admin = "Admin", Instructor = "Instructor", Student = "Student";
       
    }
}
