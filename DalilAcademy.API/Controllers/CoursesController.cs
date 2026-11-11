using DalilAcademy.Core.DTOs;
using DalilAcademy.Core.Entities;
using DalilAcademy.Core.Service;
using DalilAcademy.Core.Storage;
using DalilAcademy.Infra.Repository;
using DalilAcademy.Infra.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace DalilAcademy.API.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class CoursesController(ICourseService courseService, IImageService images) : ControllerBase
    {
        
        [HttpGet("paged")]
        public async Task<ActionResult<PageResult<CourseResponseDTO>>> GetPagedAsync(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] decimal? minPrice = null,
            [FromQuery] decimal? maxPrice = null)
        {
            page = Math.Max(page, 1);
            pageSize = Math.Clamp(pageSize, 1, 50);

            var result = await courseService.GetPagedAsync(page, pageSize, minPrice, maxPrice);
            return Ok(result);
        }
        
    
        //private readonly ICourseService _courseService;
        //public CoursesController(ICourseService courseService)
        //{
        //   _courseService = courseService;
        //}

        // CRUD
        // No Database
        // Temporary Storage - Replace by a real DB

        // GetAll
        [HttpGet]
        [AllowAnonymous]
        //public async Task<ActionResult<List<Course> GetAll() دون استخدام  ال DTO
        public async Task<ActionResult<List<CourseResponseDTO>>> GetAll()

        {
            var courses = await courseService.GetAllAsync();
            return Ok(courses);
        }

        // GetById
        //[HttpGet]
        //[Route("{id:int}")]
        //public async Task<ActionResult<Course>> GetById(int id)
        //{
        //    //var course = _courses.FirstOrDefault(c => c.Id == id);
        //    if (courseService == null)
        //    {
        //        return NotFound();
        //    }
        //    return courseService.GetById(id);
        //}
        [HttpGet]
        [AllowAnonymous]
        [Route("{id:int}")]
        //        public async Task<ActionResult<Course>> GetById(int id) دون استخدام  ال DTO

        public async Task<ActionResult<CourseResponseDTO>> GetById(int id)
        {
            var course = await courseService.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound(); // إذا لم يتم العثور على الكورس يرجع 404
            }
            return Ok(course); // إذا وجده يرجعه مع كود 200
        }
        // Create
        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        //public async Task<ActionResult<CourseResponseDTO>> Create(Course course)دون استخدام  ال DTO

        public async Task<ActionResult<CourseResponseDTO>> CreateCourseDTO(CreateCourseDTO createDto)
        {
            // course.Id = _courses.Count == 0 ? 1 : _courses.Max(c => c.Id) + 1;
            // _courses.Add(course);
            var createdCourse = await courseService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = createdCourse.Id }, createdCourse);
        }
        //// Update
        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = Roles.Admin)]

        public async Task<ActionResult> Update(int id, UpdateCourseDTO updateDto)
        {
            await courseService.UpdateAsync(id, updateDto);
            return NoContent();
        }

        // Delete
        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = Roles.Admin)]

        public async Task<ActionResult> Delete(int id)
        {
            await courseService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("{id:int}/instructor")]
        public async Task<ActionResult<Instructor>> GetInstructorByCourseId(int id)
        {

            var instructor = await courseService.GetInstructorByCourseIdAsync(id);

            if (instructor == null)
            {
                return NotFound($"No Courses {id}");
            }

            return Ok(instructor);



        }
        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        [Route("upload-image")]
        public async Task<ActionResult> UploadImage(IFormFile file)
        {
            await using var stream = file.OpenReadStream();
            var url = await images.SaveCourseImageAsync(stream, file.FileName, file.Length);
            return Ok(new {imageUrl = url});
        }
     }
}

