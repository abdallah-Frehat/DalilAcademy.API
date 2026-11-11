using DalilAcademy.Core.DTOs;
using DalilAcademy.Core.Entities;
using DalilAcademy.Core.Exceptions;
using DalilAcademy.Core.Mappings;
using DalilAcademy.Core.Repository;
using DalilAcademy.Core.Service;
using DalilAcademy.Infra.Repository;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
namespace DalilAcademy.Infra.Service
{
    public class StudentService(IStudentRepository studentRepository, 
        IValidator<CreateStudentDTO>Validator) : IStudentService
    {
        public async Task<StudentResponseDTO?> GetByUserIdAsync(string userId)
        {
            var student = await studentRepository.GetByUserIdAsync(userId);
            if (student == null) return null;

            return student.ToStudentResponseDTO();
        }

        public Task<PageResult<StudentResponseDTO>> GetPagedAsync(int page, int pageSize)
        {
            return studentRepository.GetPagedAsync(page, pageSize);
        }
        public Task<List<StudentResponseDTO>> GetAllAsync()
        {
            return studentRepository.GetAllAsync();
        }
        public async Task<StudentResponseDTO?> GetByIdAsync(int id)
        {
            var student = await studentRepository.GetByIdAsync(id)
                                ?? throw new NotFoundException($"Course {id} not found");

            if (student == null) return null;

            return student.ToStudentResponseDTO();
        }

       
        public async Task<StudentResponseDTO> CreateAsync(CreateStudentDTO createDto)
        {
            await Validator.ValidateAndThrowAsync(createDto);
            var studentEntity = createDto.ToEntity();
            var createdStudent = await studentRepository.CreateAsync(studentEntity);
            return createdStudent.ToStudentResponseDTO();
        }
        public async Task<StudentResponseDTO?> UpdateAsync(int id, UpdateStudentDTO updateDto)
        {
            var student = await studentRepository.GetByIdAsync(id)
                            ?? throw new NotFoundException($"Student {id} not found");
            if (student == null) return null;

            student.UpdateStudent(updateDto);

            await studentRepository.UpdateAsync(id, student);

            return student.ToStudentResponseDTO();
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await studentRepository.GetByIdAsync(id)
                         ?? throw new NotFoundException($"Student {id} not found");

            await studentRepository.DeleteAsync(id);
        }
        public async Task<Student?> GetStudentWithCoursesAsync(int id)
        {
            return await studentRepository.GetStudentWithCoursesAsync(id);
        }
    }

}
