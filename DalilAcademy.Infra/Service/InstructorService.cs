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
using System.Linq;
using System.Threading.Tasks;

namespace DalilAcademy.Infra.Service
{
    public class InstructorService(IInstructorRepository instructorRepository,
        IValidator<CreateInstructorDTO> Validator) : IInstructorService
    {
        public async Task<List<InstructorResponseDTO>> GetAllAsync()
        {
            var instructors = await instructorRepository.GetAllAsync();
            return instructors.Select(i => i.ToDTO()).ToList();
        }

        public async Task<InstructorResponseDTO?> GetByIdAsync(int id)
        {
            var instructor = await instructorRepository.GetByIdAsync(id)
                                ?? throw new NotFoundException($"Instructor {id} not found");

            return instructor?.ToDTO();
        }

        public async Task<InstructorResponseDTO> CreateAsync(CreateInstructorDTO createDto)
        {
            await Validator.ValidateAndThrowAsync(createDto);
            var instructorEntity = createDto.ToEntity();
            var createdInstructor = await instructorRepository.CreateAsync(instructorEntity);
            return createdInstructor.ToDTO();
        }

        public async Task<InstructorResponseDTO?> UpdateAsync(int id, UpdateInstructorDTO updateDto)
        {
            var instructor = await instructorRepository.GetByIdAsync(id)
                                ?? throw new NotFoundException($"Instructor {id} not found");

            instructor.FullName = updateDto.FullName;
            instructor.Email = updateDto.Email;

            await instructorRepository.UpdateAsync(id, instructor);

            return instructor.ToDTO();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await instructorRepository.GetByIdAsync(id)
                         ?? throw new NotFoundException($"Instructor {id} not found");

            await instructorRepository.DeleteAsync(id);
        }
    }
}