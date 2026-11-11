using DalilAcademy.Core.DTOs;
using DalilAcademy.Core.Entities;
using DalilAcademy.Core.Exceptions;
using DalilAcademy.Core.Repository;
using DalilAcademy.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DalilAcademy.Infra.Repository
{
    public class CourseRepository(AppDbContext db) : ICourseRepository
    {
       
        // استخدام الpagination
        public async Task<PageResult<CourseResponseDTO>> GetPagedAsync(int page, int pageSize, decimal? minPrice, decimal? maxPrice)
        {
            var query = db.Courses.AsQueryable();

            if (minPrice.HasValue)
            {
                query = query.Where(c => c.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(c => c.Price <= maxPrice.Value);
            }
            var count = await db.Courses.CountAsync();

            var items = await db.Courses
                .OrderBy(c => c.Id)
                .Skip((page - 1) * pageSize) 
                .Take(pageSize)
                .Select(c => new CourseResponseDTO(
                    c.Id,
                    c.Title,
                    c.Price,
                    c.Instructor!.FullName,
                    c.ImageUrl
                ))
                .ToListAsync();

            return new PageResult<CourseResponseDTO>(items, count, page, pageSize);
        }
        //-----------------------------------------------------------
        public async Task<List<CourseResponseDTO>> GetAllAsync()
        {
            // return await db.Courses.Include(c => c.Instructor).ToListAsync();استخدام  ال Include

            //projection Select استخدام ال
            var count = await db.Courses.CountAsync();// لمعرفة عدد الكورسات الموجودة في قاعدة البيانات
            return await db.Courses.Select(c => new CourseResponseDTO 
               ( c.Id, c.Title, c.Price, c.Instructor!.FullName, c.ImageUrl
            )).ToListAsync();

        }

        public async Task<Course?> GetByIdAsync(int id)
        {
            return await db.Courses
                .Include(c => c.Instructor)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Course> CreateAsync(Course course)
        {
            try
            {
                db.Courses.Add(course);
                await db.SaveChangesAsync();
                await db.Entry(course).Reference(c => c.Instructor).LoadAsync();
                return course;
            }
            catch (DbUpdateException ex) when (ex.InnerException is PostgresException { SqlState: "23505" })
            {
                throw new ConflictException("A course with this title or unique value already exists.");
            }
        }

        public async Task UpdateAsync(int id, Course updatedCourse)
        {
            try
            {
                var course = await db.Courses.FindAsync(id);
                if (course != null)
                {
                    course.Title = updatedCourse.Title;
                    course.Price = updatedCourse.Price;
                    course.InstructorId = updatedCourse.InstructorId;
                    await db.SaveChangesAsync();
                }
            }
            catch (DbUpdateException ex) when (ex.InnerException is PostgresException { SqlState: "23505" })
            {
                throw new ConflictException("A course with this title or unique value already exists.");
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var course = await db.Courses.FindAsync(id);
                if (course != null)
                {
                    db.Courses.Remove(course);
                    await db.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (DbUpdateException ex) when (ex.InnerException is PostgresException { SqlState: "23503" })
            {
                throw new ConflictException("Cannot delete the course because it is referenced by other entities.");
            }
        }

        public async Task<Instructor?> GetInstructorByCourseIdAsync(int courseId)
        {
            var course = await db.Courses
                .Include(c => c.Instructor)
                .FirstOrDefaultAsync(c => c.Id == courseId);

            return course?.Instructor;
        }
    }
}