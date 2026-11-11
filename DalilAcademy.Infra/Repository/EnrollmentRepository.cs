using DalilAcademy.Core.Entities;
using DalilAcademy.Core.Exceptions;
using DalilAcademy.Core.Repository;
using DalilAcademy.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;
namespace DalilAcademy.Infra.Repository
{
    public class EnrollmentRepository(AppDbContext db):IEnrollmentRepository
    {
        public async Task<List<Enrollment>> GetAllAsync()
        {
            return await db.Enrollments.ToListAsync();
        }

        public async Task<Enrollment?> GetByIdAsync(int id)
        {
            return await db.Enrollments.FindAsync(id);
        }

        public async Task<Enrollment> CreateAsync(Enrollment enrollment)
        {
            try
            {
                db.Enrollments.Add(enrollment);
                await db.SaveChangesAsync();
                return enrollment;
            }
            catch(DbUpdateException ex) when (ex.InnerException is PostgresException { SqlState: "23505" })
            {
                throw new ConflictException("A student is already enrolled in this course.");
            }
            
        }

        public async Task UpdateAsync(int id, Enrollment updatedEnrollment)
        {
            try
            {

                var enrollment = await db.Enrollments.FindAsync(id);
                if (enrollment != null)
                {
                    enrollment.StudentId = updatedEnrollment.StudentId;
                    enrollment.CourseId = updatedEnrollment.CourseId;
                    enrollment.EnrolledAt = updatedEnrollment.EnrolledAt;
                    await db.SaveChangesAsync();
                }
            }
            catch (DbUpdateException ex) when (ex.InnerException is PostgresException { SqlState: "23505" })
            {
                throw new ConflictException("A student is already enrolled in this course.");
            }
        }

        public async Task<bool>DeleteAsync(int id)
        {
 
            var enrollment = await db.Enrollments.FindAsync(id);
            if (enrollment != null)
            {
                db.Enrollments.Remove(enrollment);
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}   
    
