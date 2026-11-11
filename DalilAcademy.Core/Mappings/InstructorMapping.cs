using DalilAcademy.Core.DTOs;
using DalilAcademy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DalilAcademy.Core.Mappings
{
    public static class InstructorMapping
    {
        public static InstructorResponseDTO ToDTO(this Instructor instructor)
        {
            return new InstructorResponseDTO(instructor.Id, instructor.FullName, instructor.Email);
        }
        public static Instructor ToEntity(this CreateInstructorDTO instructorDTO)
        {
            return new Instructor
            {
                FullName = instructorDTO.FullName,
                Email = instructorDTO.Email
            };
        }
        public static Instructor ToEntity(this  Instructor instructor)
        {
            instructor.FullName = instructor.FullName;
            instructor.Email = instructor.Email;
            return instructor;
        }
    }
}
