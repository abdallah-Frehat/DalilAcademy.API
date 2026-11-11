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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalilAcademy.Infra.Service
{
    public class EnrollmentService(IEnrollmentRepository enrollmentRepository,
        IValidator<CreateEnrollmentDTO> Validator) : IEnrollmentService
    {
        public async Task<List<EnrollmentResponseDTO>> GetAllAsync()
        {
            var enrollments = await enrollmentRepository.GetAllAsync();
            return enrollments.Select(e => e.ToDTO()).ToList();
        }

        public async Task<EnrollmentResponseDTO?> GetByIdAsync(int id)
        {
            var enrollment = await enrollmentRepository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Enrollment {id} not found");
            return enrollment?.ToDTO();
        }

        public async Task<EnrollmentResponseDTO> CreateAsync(CreateEnrollmentDTO createDto)
        {
            await Validator.ValidateAndThrowAsync(createDto);
            var enrollmentEntity = createDto.ToEntity();
            var createdEnrollment = await enrollmentRepository.CreateAsync(enrollmentEntity);
            return createdEnrollment.ToDTO();
        }

        public async Task<EnrollmentResponseDTO?> UpdateAsync(int id, UpdateEnrollmentDTO updateDto)
        {
            var enrollment = await enrollmentRepository.GetByIdAsync(id)
                                ?? throw new NotFoundException($"Enrollment {id} not found");

            enrollment.StudentId = updateDto.StudentId;
            enrollment.CourseId = updateDto.CourseId;

            await enrollmentRepository.UpdateAsync(id, enrollment);

            return enrollment.ToDTO();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await enrollmentRepository.GetByIdAsync(id)
                         ?? throw new NotFoundException($"Enrollment {id} not found");

            await enrollmentRepository.DeleteAsync(id);
        }
    }
}