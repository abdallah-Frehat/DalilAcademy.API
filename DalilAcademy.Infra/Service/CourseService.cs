using DalilAcademy.Core.DTOs;
using DalilAcademy.Core.Entities;
using DalilAcademy.Core.Exceptions;
using DalilAcademy.Core.Mappings;
using DalilAcademy.Core.Repository;
using DalilAcademy.Core.Service;
using DalilAcademy.Infra.Repository;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DalilAcademy.Infra.Service
{
    public class CourseService(ICourseRepository courseRepository,
        IValidator<CreateCourseDTO> Validator) : ICourseService
    {
        public Task<PageResult<CourseResponseDTO>> GetPagedAsync(int page, int pageSize, decimal? minPrice, decimal? maxPrice)
        {
            return courseRepository.GetPagedAsync(page, pageSize, minPrice, maxPrice);
        }

        public Task<List<CourseResponseDTO>> GetAllAsync()
        {
            return courseRepository.GetAllAsync();
        }

        public async Task<CourseResponseDTO?> GetByIdAsync(int id)
        {
            var courseEntity = await courseRepository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Course {id} not found");
            return courseEntity.ToDTO();
        }

        public async Task<CourseResponseDTO> CreateAsync(CreateCourseDTO createDto)
        {
            await Validator.ValidateAndThrowAsync(createDto);
            var courseEntity = createDto.ToEntity();
            var createdCourse = await courseRepository.CreateAsync(courseEntity);
            return createdCourse.ToDTO();
        }

        public async Task<CourseResponseDTO?> UpdateAsync(int id, UpdateCourseDTO updateDto)
        {
            var courseEntity = await courseRepository.GetByIdAsync(id)
                                ?? throw new NotFoundException($"Course {id} not found");

            courseEntity.Title = updateDto.Title;
            courseEntity.Price = updateDto.Price;

            await courseRepository.UpdateAsync(id, courseEntity);

            return courseEntity.ToDTO();
        }

        public async Task DeleteAsync(int id)
        {
            if (!await courseRepository.DeleteAsync(id))
                throw new NotFoundException($"Course {id} was not found.");
        }

        public async Task<Instructor?> GetInstructorByCourseIdAsync(int courseId)
        {
            return await courseRepository.GetInstructorByCourseIdAsync(courseId);
        }
    }
}