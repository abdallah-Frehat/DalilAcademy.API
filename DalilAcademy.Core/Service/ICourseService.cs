using DalilAcademy.Core.DTOs;
using DalilAcademy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
namespace DalilAcademy.Core.Service
{
    public interface ICourseService
    {
        Task<PageResult<CourseResponseDTO>> GetPagedAsync(int page, int pageSize, decimal? minPrice, decimal? maxPrice);
        Task<List<CourseResponseDTO>> GetAllAsync();
        Task<CourseResponseDTO?> GetByIdAsync(int id);
        Task<CourseResponseDTO> CreateAsync(CreateCourseDTO createDto);
        Task<CourseResponseDTO?> UpdateAsync(int id, UpdateCourseDTO updateDto);
        Task DeleteAsync(int id);
        Task<Instructor?> GetInstructorByCourseIdAsync(int courseId);

    }
}
