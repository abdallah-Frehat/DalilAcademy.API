using DalilAcademy.Core.DTOs;
using DalilAcademy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DalilAcademy.Core.Mappings
{
    public static class StudentMapping
    {
        public static StudentResponseDTO ToStudentResponseDTO(this Student student)
        {
            return new StudentResponseDTO(student.Id, student.FullName, student.Email, student.PhoneNumber);
        }
        public static Student ToEntity(this CreateStudentDTO createStudentDTO)
        {
            return new Student
            {
                FullName = createStudentDTO.FullName,
                Email = createStudentDTO.Email,
                PhoneNumber = createStudentDTO.PhoneNumber
            };
        }
        public static void UpdateStudent(this Student student, UpdateStudentDTO updateStudentDTO)
        {
            student.FullName = updateStudentDTO.FullName;
            student.Email = updateStudentDTO.Email;
            student.PhoneNumber = updateStudentDTO.PhoneNumber;
        }
    }
}
