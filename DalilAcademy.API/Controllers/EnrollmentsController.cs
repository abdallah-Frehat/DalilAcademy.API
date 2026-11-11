using DalilAcademy.Core.DTOs;
using DalilAcademy.Core.Entities;
using DalilAcademy.Core.Service;
using DalilAcademy.Infra.Data;
using DalilAcademy.Infra.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


namespace DalilAcademy.API.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class EnrollmentsController(IEnrollmentService enrollmentService, AppDbContext db) : ControllerBase
    {
        //private readonly EnrollmentService _enrollmentService;
        //public EnrollmentsController(EnrollmentService enrollmentService)
        //{
        //   _enrollmentService = enrollmentService;
        //}

        // CRUD
        // No Database
        // Temporary Storage - Replace by a real DB

        // GetAll
        [HttpGet]
        //public async Task<ActionResult<List<Enrollment>>> GetAll()دون استخدام  ال DTO
        public async Task<ActionResult<List<EnrollmentResponseDTO>>> GetAll()

        {
            var enrollments = await enrollmentService.GetAllAsync();
            return Ok(enrollments);
        }

        // GetById
        //[HttpGet]
        //[Route("{id:int}")]
        //public ActionResult<Enrollment> GetById(int id)
        //{
        //    //var enrollment = _enrollments.FirstOrDefault(e => e.Id == id);
        //    if (enrollmentService == null)
        //    {
        //        return NotFound();
        //    }
        //    return enrollmentService.GetById(id);
        //}
        [HttpGet]
        [Route("{id:int}")]
        //public async Task<ActionResult<Enrollment>> GetById(int id)دون استخدام  ال DTO

        public async Task<ActionResult<EnrollmentResponseDTO>> GetById(int id)
        {
            var enrollment = await enrollmentService.GetByIdAsync(id);
            if (enrollment == null)
            {
                return NotFound(); // إذا لم نعثر على السجل يرجع 404
            }
            return Ok(enrollment); // إذالقيته يرجعه مع كود 200
        }
        // Create
        [HttpPost]
        //public async Task<ActionResult<Enrollment>> Create(Enrollment enrollment)
        public async Task<ActionResult<EnrollmentResponseDTO>> Create(CreateEnrollmentDTO createDto)
        {
            // enrollment.Id = _enrollments.Count == 0 ? 1 : _enrollments.Max(e => e.Id) + 1;
            // _enrollments.Add(enrollment);
            var createdEnrollment = await enrollmentService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = createdEnrollment.Id }, createdEnrollment);
        }
        //// Update
        [HttpPut]
        [Route("{id:int}")]
        //public async Task<ActionResult> Update(int id, Enrollment updatedEnrollment)دون استخدام  ال DTO
        public async Task<ActionResult<EnrollmentResponseDTO>> Update(int id, UpdateEnrollmentDTO updateDto)
        {
            await enrollmentService.UpdateAsync(id, updateDto);
            return NoContent();
        }

        // Delete
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await enrollmentService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("me")]
        public async Task<ActionResult<List<EnrollmentResponseDTO>>> GetMyEnrollments()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized();
            }
            var student = await db.Students.FirstOrDefaultAsync(s => s.UserId == userId);
            if (student == null)
            {
                return NotFound("Student not found");
            }
            var enrollments = await db.Enrollments
        .Where(e => e.StudentId == student.Id)
        .Include(e => e.Course)
        .Select(e => new EnrollmentResponseDTO(
            e.Id,
            e.StudentId,
            e.CourseId,
            e.EnrolledAt
        ))
        .ToListAsync();
            return Ok(enrollments);
        }

    }
}


