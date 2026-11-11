using DalilAcademy.Core.DTOs;
using DalilAcademy.Core.Entities;

public interface IStudentRepository
{
    Task<PageResult<StudentResponseDTO>> GetPagedAsync(int page, int pageSize);
    Task<List<StudentResponseDTO>> GetAllAsync(); Task<Student?> GetByIdAsync(int id);
    Task<Student> CreateAsync(Student student);
    Task UpdateAsync(int id, Student updatedStudent);
    Task<bool> DeleteAsync(int id);
    Task<Student?> GetStudentWithCoursesAsync(int id);
    Task AddAsync(Student newStudent);
    Task<Student?> GetByUserIdAsync(string userId);
}