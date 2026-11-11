using DalilAcademy.Core.DTOs;
using DalilAcademy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DalilAcademy.Core.Mappings
{
    public static class EnrollmentMapping
    {
        public static EnrollmentResponseDTO ToDTO(this Enrollment enrollment)
        {
            return new EnrollmentResponseDTO(enrollment.Id, enrollment.StudentId, enrollment.CourseId, enrollment.EnrolledAt);
        }
        public static Enrollment ToEntity(this CreateEnrollmentDTO enrollmentDTO)
        {
            return new Enrollment
            {
                StudentId = enrollmentDTO.StudentId,
                CourseId = enrollmentDTO.CourseId,
                EnrolledAt = DateTime.UtcNow
            };
        }
        public static Enrollment ToEntity(this UpdateEnrollmentDTO enrollmentDTO)
        {
            return new Enrollment
            {
                StudentId = enrollmentDTO.StudentId,
                CourseId = enrollmentDTO.CourseId,
                EnrolledAt = DateTime.UtcNow
            };
        }
    }
}
