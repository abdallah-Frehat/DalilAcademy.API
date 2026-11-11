using DalilAcademy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DalilAcademy.Core.Repository
{
    public interface IInstructorRepository
    {
        Task<List<Instructor>> GetAllAsync();
        Task<Instructor?> GetByIdAsync(int id);
        Task<Instructor> CreateAsync(Instructor instructor);
        Task UpdateAsync(int id, Instructor updatedInstructor);
        Task<bool> DeleteAsync(int id);
        
    }
}

