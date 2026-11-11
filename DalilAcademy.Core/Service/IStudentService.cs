using DalilAcademy.Core.DTOs;
using DalilAcademy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DalilAcademy.Core.Service
{
    public interface IStudentService
    {
        Task<PageResult<StudentResponseDTO>> GetPagedAsync(int page, int pageSize);
        Task<List<StudentResponseDTO>> GetAllAsync();
        Task<StudentResponseDTO?> GetByIdAsync(int id);
        Task<StudentResponseDTO> CreateAsync(CreateStudentDTO createDto);
        Task<StudentResponseDTO?> UpdateAsync(int id, UpdateStudentDTO updateDto);
        Task DeleteAsync(int id);
        Task<Student?> GetStudentWithCoursesAsync(int id);
        Task<StudentResponseDTO?> GetByUserIdAsync(string userId);
    }
}
