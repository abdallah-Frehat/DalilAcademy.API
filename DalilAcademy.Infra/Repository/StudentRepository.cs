using DalilAcademy.Core.DTOs;
using DalilAcademy.Core.Entities;
using DalilAcademy.Core.Exceptions;
using DalilAcademy.Core.Repository;
using DalilAcademy.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace DalilAcademy.Infra.Repository
{
    public class StudentRepository(AppDbContext db) : IStudentRepository
    {
        public async Task<PageResult<StudentResponseDTO>> GetPagedAsync(int page, int pageSize)
        {
            var count = await db.Students.CountAsync();

            var items = await db.Students
                .OrderBy(s => s.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new StudentResponseDTO(
                    s.Id,
                    s.FullName,
                    s.Email,
                    s.PhoneNumber
                ))
                .ToListAsync();

            return new PageResult<StudentResponseDTO>(items, page, pageSize, count);
        }

        public async Task<List<StudentResponseDTO>> GetAllAsync()
        {
            return await db.Students.Select(s => new StudentResponseDTO(
                s.Id,
                s.FullName,
                s.Email,
                s.PhoneNumber
            )).ToListAsync();
        }
        public async Task<Student?> GetByIdAsync(int id)
        {
            return await db.Students.FindAsync(id);
        }

        public async Task<Student> CreateAsync(Student student)
        {
            try
            {
                db.Students.Add(student);
                await db.SaveChangesAsync();
                return student;
            }
            catch (DbUpdateException ex) when (ex.InnerException is PostgresException { SqlState: "23505" })
            {
                throw new ConflictException("A student with this email or unique value already exists.");
            }
        }

        public async Task UpdateAsync(int id, Student updatedStudent)
        {
            try
            {
                var student = await db.Students.FindAsync(id);
                if (student != null)
                {
                    student.FullName = updatedStudent.FullName;
                    student.Email = updatedStudent.Email;
                    student.PhoneNumber = updatedStudent.PhoneNumber;
                    await db.SaveChangesAsync();
                }
            }
            catch (DbUpdateException ex) when (ex.InnerException is PostgresException { SqlState: "23505" })
            {
                throw new ConflictException("A student with this email or unique value already exists.");
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var student = await db.Students.FindAsync(id);
                if (student != null)
                {
                    db.Students.Remove(student);
                    await db.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (DbUpdateException ex) when (ex.InnerException is PostgresException { SqlState: "23503" })
            {
                throw new ConflictException("Cannot delete the student because it is referenced by other records.");
            }
        }

        public async Task<Student?> GetStudentWithCoursesAsync(int id)
        {
            return await db.Students
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddAsync(Student newStudent)
        {
            await db.Students.AddAsync(newStudent);
            await db.SaveChangesAsync();
        }

        public async Task<Student?> GetByUserIdAsync(string userId)
        {
            return await db.Students
             .FirstOrDefaultAsync(s => s.UserId == int.Parse(userId));
        }
    }
}