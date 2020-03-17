using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities;
using Core.Specifications.StudentSpecifications;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
            return CreatedAtRoute("GetStudent", new { id = newStudent.Id }, _mapper.Map<SimpleStudentDTO>(newStudent));
        }

        // api/students/{id:int}
        [HttpGet("{id}", Name = "GetStudent")]
        public async Task<IActionResult> GetStudent([FromRoute] int id)
        {
            var qro = await _uow.Repository<Student>().Find(new DetailStudentSpecification(id));
            if (qro == null || qro.QueryResult == null)
            {
                if (qro.QueryResult == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
                return NotFound();
            }
            return Ok(_mapper.Map<DetailStudentDTO>(qro.QueryResult.Single()));
        }

        // api/students/
        [HttpGet]
        public async Task<IActionResult> GetStudents(
            // [FromQuery] SearchOption searchOption
            [FromQuery] OrderingOption orderingOption,
            [FromQuery] PagingOption pagingOption
        )
        {
            var specification = new SimpleStudentsSpecification(
                    orderingOption.GetOrderByInfos(),
                    pagingOption.CurrentPage, pagingOption.PageSize
                );
            var qro = await _uow.Repository<Student>().Find(specification);
            if (qro == null || qro.QueryResult == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(_mapper.Map<SimpleStudentsResultDTO>(qro));
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
            return Ok(_mapper.Map<SimpleStudentDTO>(student));
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