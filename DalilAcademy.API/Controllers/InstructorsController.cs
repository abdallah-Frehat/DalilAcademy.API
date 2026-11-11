using DalilAcademy.Core.DTOs;
using DalilAcademy.Core.Entities;
using DalilAcademy.Core.Service;
using DalilAcademy.Infra.Service;
using Microsoft.AspNetCore.Mvc;


namespace DalilAcademy.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class InstructorsController(IInstructorService instructorService) : ControllerBase
    {
        //private readonly InstructorService _instructorService;
        //public InstructorsController(InstructorService instructorService)
        //{
        //   _instructorService = instructorService;
        //}

        // CRUD
        // No Database
        // Temporary Storage - Replace by a real DB

        // GetAll
        [HttpGet]
        //public async Task<ActionResult<List<Instructor>>> GetAllAsync()دون استخدام  ال DTO
        public async Task<ActionResult<List<InstructorResponseDTO>>> GetAllAsync()
        {
            return Ok(await instructorService.GetAllAsync());
        }

        // GetById
        //[HttpGet]
        //[Route("{id:int}")]
        //public ActionResult<Instructor> GetById(int id)
        //{
        //    //var instructor = _instructors.FirstOrDefault(i => i.Id == id);
        //    if (instructorService == null)
        //    {
        //        return NotFound();
        //    }
        //    return instructorService.GetById(id);
        //}
        [HttpGet]
        [Route("{id:int}")]
        //public async Task<ActionResult<Instructor>> GetByIdAsync(int id)دون استخدام  ال DTO
        public async Task<ActionResult<InstructorResponseDTO>> GetByIdAsync(int id)
        {
            var instructor = await instructorService.GetByIdAsync(id);
            if (instructor == null)
            {
                return NotFound(); // إذا لم نعثر على المدرس يرجع 404
            }
            return Ok(instructor); // إذالقيته يرجعه مع كود 200
        }
        // Create
        [HttpPost]
        //public async Task<ActionResult<Instructor>> CreateAsync(Instructor instructor)دون استخدام  ال DTO
        public async Task<ActionResult<InstructorResponseDTO>> CreateAsync(CreateInstructorDTO createDto)
        {
            // instructor.Id = _instructors.Count == 0 ? 1 : _instructors.Max(i => i.Id) + 1;
            // _instructors.Add(instructor);
            var createdInstructor = await instructorService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = createdInstructor.Id }, createdInstructor);
        }
        //// Update
        [HttpPut]
        [Route("{id:int}")]
        //public async Task<ActionResult> UpdateAsync(int id, Instructor updatedInstructor)دون استخدام  ال DTO
        public async Task<ActionResult<InstructorResponseDTO>> UpdateAsync(int id, UpdateInstructorDTO updateDto)
        {
            await instructorService.UpdateAsync(id, updateDto);
            return NoContent();
        }

        // Delete
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            await instructorService.DeleteAsync(id);
            return NoContent();
        }
    }
}


