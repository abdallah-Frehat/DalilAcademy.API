using DalilAcademy.Core.DTOs;
using DalilAcademy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DalilAcademy.Core.Repository
{
    public interface ICourseRepository
    {
        Task<PageResult<CourseResponseDTO>> GetPagedAsync(int page, int pageSize, decimal? minPrice, decimal? maxPrice);
        Task <List<CourseResponseDTO>> GetAllAsync();
        Task<Course ?> GetByIdAsync(int id);
        Task<Course> CreateAsync(Course course);
        Task UpdateAsync(int id, Course updatedCourse);
        Task<bool> DeleteAsync(int id);
        Task<Instructor?> GetInstructorByCourseIdAsync(int courseId);
        
    }
}
