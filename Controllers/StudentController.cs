using API_.NET_CRUD_Minimal_E.F_ORM.DTOs.Student;
using API_.NET_CRUD_Minimal_E.F_ORM.DTOs.Student.Validation;
using API_.NET_CRUD_Minimal_E.F_ORM.Models;
using API_.NET_CRUD_Minimal_E.F_ORM.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace API_.NET_CRUD_Minimal_E.F_ORM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly CreateStudentDTOValidator _createStudentDtoValidator;
        private readonly IValidator<int> _idValidator;

        public StudentController(IStudentService studentService, CreateStudentDTOValidator createStudentDtoValidator, IValidator<int> idValidator)
        {
            _studentService = studentService;
            _createStudentDtoValidator = createStudentDtoValidator;
            _idValidator = idValidator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDTO>> GetById(int id)
        {

            var validationResult = await _idValidator.ValidateAsync(id);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var student = await _studentService.GetByIdAsync(id);
            if (student == null)
                return NotFound();

            return Ok(new StudentDTO
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName
            });
        }

        [HttpGet]
        public async Task<ActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var students = await _studentService.GetAllAsync(pageNumber, pageSize);
            var totalStudents = await _studentService.GetTotalCountAsync();
            var totalPages = (int)Math.Ceiling(totalStudents / (double)pageSize);

            var studentDtos = students.Select(s => new StudentDTO
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName
            });

            var result = new
            {
                TotalRecords = totalStudents,
                TotalPages = totalPages,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                Students = studentDtos
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] CreateStudentDTO createStudentDto)
        {

            var validationResult = await _createStudentDtoValidator.ValidateAsync(createStudentDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var student = new Student
            {
                FirstName = createStudentDto.FirstName,
                LastName = createStudentDto.LastName
            };

            var studentDto = await _studentService.AddAsync(student);

            return CreatedAtAction(nameof(GetById), new { id = studentDto.Id }, studentDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] CreateStudentDTO createStudentDto)
        {

            var validationResult = await _idValidator.ValidateAsync(id);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var student = await _studentService.GetByIdAsync(id);
            if (student == null)
                return NotFound();

            student.FirstName = createStudentDto.FirstName;
            student.LastName = createStudentDto.LastName;

            await _studentService.UpdateAsync(student);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {

            var validationResult = await _idValidator.ValidateAsync(id);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var student = await _studentService.GetByIdAsync(id);
            if (student == null)
                return NotFound();

            await _studentService.DeleteAsync(id);
            return NoContent();
        }
    }
}
