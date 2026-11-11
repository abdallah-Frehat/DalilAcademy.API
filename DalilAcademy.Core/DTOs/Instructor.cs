using System;
using System.Collections.Generic;
using System.Text;

namespace DalilAcademy.Core.DTOs
{
   public record CreateInstructorDTO(string FullName, string Email);
   public record UpdateInstructorDTO(string FullName, string Email);
   public record InstructorResponseDTO(int Id, string FullName, string Email);
}
