using System;
using System.Collections.Generic;
using System.Text;

namespace DalilAcademy.Core.DTOs
{
    public record CreateCourseDTO(string Title, decimal Price, int InstructorId, string? ImageUrl);
    public record UpdateCourseDTO(string Title, decimal Price, string? ImageUrl);
    public record CourseResponseDTO(int Id, string Title, decimal Price, string InstructorName, string? ImageUrl);
    
}
