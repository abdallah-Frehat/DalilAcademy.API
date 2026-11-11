using DalilAcademy.Core.DTOs;
using DalilAcademy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DalilAcademy.Core.Service
{
    public interface IInstructorService
    {
        Task<List<InstructorResponseDTO>> GetAllAsync();
        Task<InstructorResponseDTO?> GetByIdAsync(int id);
        Task<InstructorResponseDTO> CreateAsync(CreateInstructorDTO createDto);
        Task<InstructorResponseDTO?> UpdateAsync(int id, UpdateInstructorDTO updateDto);
        Task DeleteAsync (int id);

    }
}
