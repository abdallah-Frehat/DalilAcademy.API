using DalilAcademy.Core.DTOs;
using DalilAcademy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DalilAcademy.Core.Service
{
    public interface IEnrollmentService
    {
        Task<List<EnrollmentResponseDTO>> GetAllAsync();
        Task<EnrollmentResponseDTO?> GetByIdAsync(int id);
        Task<EnrollmentResponseDTO> CreateAsync(CreateEnrollmentDTO createDto);
        Task<EnrollmentResponseDTO?> UpdateAsync(int id, UpdateEnrollmentDTO updateDto);
        Task DeleteAsync(int id);
        
    }
}
