using System;
using System.Collections.Generic;
using System.Text;

namespace DalilAcademy.Core.Entities
{
    public class Instructor
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public List<Course> Courses { get; set; } = [];
        
      

    }
}
