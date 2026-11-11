using System;
using System.Collections.Generic;
using System.Text;

namespace DalilAcademy.Core.DTOs
{
    public record CreateEnrollmentDTO(int StudentId, int CourseId);
    public record UpdateEnrollmentDTO(int StudentId, int CourseId);
    public record EnrollmentResponseDTO(int Id, int StudentId, int CourseId, DateTime EnrolledAt);
    
}
