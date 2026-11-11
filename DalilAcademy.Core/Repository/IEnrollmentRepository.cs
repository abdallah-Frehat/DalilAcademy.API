using DalilAcademy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DalilAcademy.Core.Repository
{
    public interface IEnrollmentRepository
    {
        Task<List<Enrollment>> GetAllAsync();
        Task<Enrollment?> GetByIdAsync(int id);
        Task<Enrollment> CreateAsync(Enrollment enrollment);
        Task UpdateAsync(int id, Enrollment updatedEnrollment);
        Task<bool> DeleteAsync(int id);
        
    }
}
