using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities;
using Core.Specifications;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAPI.DTOs;
using WebAPI.DTOs.StudentDTOs;
using WebAPI.Helpers;

namespace WebAPI.Controllers
{
    public class StudentsController : ApiControllerBase<StudentsController>
    {
        public StudentsController(ILogger<StudentsController> logger, IMapper mapper, IUnitOfWork uow) : base(logger, mapper, uow) { }

        // api/students/
        [HttpPost]
        public IActionResult PostStudent([FromBody] PostStudentDTO body)
        {
            var newStudent = _mapper.Map<Student>(body);
            _uow.Repository<Student>().Add(newStudent);

            if (!_uow.Complete(1))
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return CreatedAtRoute("GetStudent", new { id = newStudent.Id }, _mapper.Map<StudentOnlyDTO>(newStudent));
        }

        // api/students/{id:int}
        [HttpGet("{id}", Name = "GetStudent")]
        public async Task<IActionResult> GetStudent([FromRoute] int id)
        {
            var student = await _uow.Repository<Student>().Find(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<StudentOnlyDTO>(student));
        }

        // api/students/
        [HttpGet]
        public async Task<IActionResult> GetStudents(
            // [FromQuery] SearchOption searchOption
            [FromQuery] OrderingOption orderingOption
        )
        {
            var students = await _uow.Repository<Student>().Find(
                new StudentsOnlySpecification(orderingOption.GetOrderByInfos(), 0, 0)
            );
            if (students == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(_mapper.Map<List<StudentOnlyDTO>>(students));
        }

        // api/students/{id:int}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent([FromRoute] int id, [FromBody] PutStudentDTO body)
        {
            var student = await _uow.Repository<Student>().Find(id);
            if (student == null)
            {
                return NotFound();
            }

            student = _mapper.Map(body, student);
            _uow.Repository<Student>().Update(student);

            if (!_uow.Complete(1))
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(_mapper.Map<StudentOnlyDTO>(student));
        }

        // api/students/{id:int}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent([FromRoute] int id)
        {
            var student = await _uow.Repository<Student>().Find(id);
            if (student == null)
            {
                return NotFound();
            }

            _uow.Repository<Student>().Remove(student);
            if (!_uow.Complete(1))
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok();
        }
    }
}