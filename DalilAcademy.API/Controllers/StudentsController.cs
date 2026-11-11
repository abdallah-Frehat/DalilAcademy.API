using DalilAcademy.Core.DTOs;
using DalilAcademy.Core.Entities;
using DalilAcademy.Core.Service;
using DalilAcademy.Infra.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


    namespace DalilAcademy.API.Controllers
{

        [Route("api/[controller]")]
        [ApiController]

        
        //public class StudentsController(IStudentService studentService) : ControllerBase
          public class StudentsController(IStudentService studentService) : ControllerBase
        {
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<StudentResponseDTO>> GetByUserId(string userId)
        {
            var studentResponse = await studentService.GetByUserIdAsync(userId);

            if (studentResponse == null)
            {
                return NotFound(new { message = "Student not found for this user ID." });
            }

            return Ok(studentResponse);
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<ActionResult<StudentResponseDTO>> GetMyProfile()
          {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }
            var student = await studentService.GetByUserIdAsync(userId);
            if (student == null)
            {
                return NotFound("Student profile not found.");
            }

            return Ok(student);
        }

        [HttpGet("paged")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<PageResult<StudentResponseDTO>>> GetPagedAsync(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            page = Math.Max(page, 1);
            pageSize = Math.Clamp(pageSize, 1, 50);

            var result = await studentService.GetPagedAsync(page, pageSize);
            return Ok(result);
        }


        //private readonly IStudentService _studentService;
        //public StudentsController(IStudentService studentService)
        //{
        //   _studentService = studentService;
        //}

        // CRUD
        // No Database
        // Temporary Storage - Replace by a real DB

        // GetAll
        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        //public async Task<ActionResult<List<Student>>> GetAll()دون استخدام  ال DTO
        public async Task<ActionResult<List<StudentResponseDTO>>> GetAll()
            {
                var students = await studentService.GetAllAsync();
                return Ok(students);
            }

            // GetById
            //[HttpGet]
            //[Route("{id:int}")]
            //public async Task<ActionResult<Student>   > GetById(int id)
            //{
            //    //var student = _students.FirstOrDefault(s => s.Id == id);
            //    if (studentService == null)
            //    {
            //        return NotFound();
            //    }
            //    return studentService.GetById(id);
            //}
            [HttpGet]
            [Route("{id:int}")]
        [Authorize(Roles = Roles.Admin)]
        //public async Task<ActionResult<Student>> GetById(int id)دون استخدام  ال DTO
        public async Task<ActionResult<StudentResponseDTO>> GetById(int id)
            {
                var student = await studentService.GetByIdAsync(id);
                if (student == null)
                {
                    return NotFound(); // إذا لم نعثر على الطالب يرجع 404
                }
                return Ok(student); // إذالقيته يرجعه مع كود 200
            }
            // Create
            [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        //public async Task<ActionResult<Student>> Create(Student student)دون استخدام  ال DTO
        public async Task<ActionResult<StudentResponseDTO>> Create(CreateStudentDTO createDto)
            {
                // student.Id = _students.Count == 0 ? 1 : _students.Max(s => s.Id) + 1;
                // _students.Add(student);
                var createdStudent = await studentService.CreateAsync(createDto);
                return CreatedAtAction(nameof(GetById), new { id = createdStudent.Id }, createdStudent);
            }
            //// Update
            [HttpPut]
            [Route("{id:int}")]
        [Authorize(Roles = Roles.Admin)]
        //public async Task<ActionResult> Update(int id, Student updatedStudent)دون استخدام  ال DTO
        public async Task<ActionResult<StudentResponseDTO>> Update(int id, UpdateStudentDTO updateDto)
            {
                await studentService.UpdateAsync(id, updateDto);
                return NoContent();
            }

        // Delete

        [HttpDelete]
            [Route("{id:int}")]
        [Authorize(Roles = Roles.Admin)]

        public async Task<ActionResult> DeleteAsync(int id)
            {
                await studentService.DeleteAsync(id);
                return NoContent();
            }
        [HttpGet("{id:int}/courses")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<IEnumerable<Course>>> GetCoursesByStudentId(int id)
        {
            var student = await studentService.GetStudentWithCoursesAsync(id);

            if (student == null)
            {
                return NotFound($"The Student Does Not Exist {id} ");
            }

            var courses = student.Enrollments.Select(e => e.Course).ToList();
            return Ok(courses);
        }
    }
    }

