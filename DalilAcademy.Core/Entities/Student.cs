using System;
using System.Collections.Generic;
using System.Text;

namespace DalilAcademy.Core.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public List<Enrollment> Enrollments { get; set; } = [];
        public int UserId { get; set; }         //FKey
        public User User { get; set; } = null!;



    }
}
