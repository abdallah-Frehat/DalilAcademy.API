using System;
using System.Collections.Generic;
using System.Text;

namespace DalilAcademy.Core.DTOs
{
    public record CreateStudentDTO(string FullName, string Email, string PhoneNumber, string UserId);
    public record UpdateStudentDTO(string FullName, string Email, string PhoneNumber);
   public record StudentResponseDTO(int Id, string FullName, string Email, string PhoneNumber);
}
