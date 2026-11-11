using DalilAcademy.Core.Entities;
using DalilAcademy.Core.Exceptions;
using DalilAcademy.Core.Repository;
using DalilAcademy.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;
namespace DalilAcademy.Infra.Repository
{
    public class InstructorRepository(AppDbContext db) : IInstructorRepository
    {

        public async Task<List<Instructor>> GetAllAsync()
        {
            return await db.Instructors.ToListAsync();
        }

        public async Task<Instructor?> GetByIdAsync(int id)
        {

            return await db.Instructors.FindAsync(id);
        }

        public async Task<Instructor> CreateAsync(Instructor instructor)
        {
            try
            {
                db.Instructors.Add(instructor);
                await db.SaveChangesAsync();
                return instructor;
            }
            catch (DbUpdateException ex) when (ex.InnerException is PostgresException { SqlState: "23505" })
            {
                throw new ConflictException("A instructor with this email or unique value already exists.");
            }
        }

        public async Task UpdateAsync(int id, Instructor updatedInstructor)
        {
            try
            {
                var instructor = await db.Instructors.FindAsync(id);
                if (instructor != null)
                {
                    instructor.FullName = updatedInstructor.FullName;
                    instructor.Email = updatedInstructor.Email;
                    await db.SaveChangesAsync();
                }
            }
            catch (DbUpdateException ex) when (ex.InnerException is PostgresException { SqlState: "23505" })
            {
                throw new ConflictException("A instructor with this email or unique value already exists.");
            }
        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var instructor = await db.Instructors.FindAsync(id);
                if (instructor != null)
                {
                    db.Instructors.Remove(instructor);
                    await db.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (DbUpdateException ex) when (ex.InnerException is PostgresException { SqlState: "23503" })
            {
                throw new ConflictException("Cannot delete instructor because it is referenced by other entities.");
            }
        }
    }
}         
         
