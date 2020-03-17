using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities;
using Core.Specifications;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAPI.DTOs.EnrolmentDTOs;
using WebAPI.Helpers;

namespace WebAPI.Controllers
{
    public class EnrolmentsController : ApiControllerBase<EnrolmentsController>
    {
        public EnrolmentsController(ILogger<EnrolmentsController> logger, IMapper mapper, IUnitOfWork uow) : base(logger, mapper, uow) { }

        // api/enrolments/
        [HttpPost]
        public async Task<IActionResult> PostEnrolment([FromBody] PostEnrolmentDTO body)
        {
            var getStudent = _uow.Repository<Student>().Find(body.StudentId);
            var getLesson = _uow.Repository<Lesson>().Find(body.LessonId);

            var student = await getStudent;
            var lesson = await getLesson;
            if (student == null || lesson == null)
            {
                return NotFound();
            }

            if (await _uow.Repository<Enrolment>().Find(student.Id, lesson.Id) != null)
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }

            var newEnrolment = new Enrolment
            {
                LessonId = lesson.Id,
                Lesson = lesson,
                StudentId = student.Id,
                Student = student
            };
            _uow.Repository<Enrolment>().Add(newEnrolment);

            if (!_uow.Complete(1))
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return StatusCode(StatusCodes.Status201Created, _mapper.Map<EnrolmentDTO>(newEnrolment));
        }

        // api/enrolments/
        [HttpGet]
        public async Task<IActionResult> GetEnrolments(
            [FromQuery] OrderingOption orderingOption,
            [FromQuery] PagingOption pagingOption
        )
        {
            var specification = new EnrolmentsSpecification(
                    orderingOption.GetOrderByInfos(),
                    pagingOption.CurrentPage, pagingOption.PageSize
                );
            var qro = await _uow.Repository<Enrolment>().Find(specification);
            if (qro == null || qro.QueryResult == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(_mapper.Map<EnrolmentsResultDTO>(qro));
        }

        // api/enrolments/students/{studentId:int}/lessons/{lessonId:int}
        [HttpDelete("students/{studentId}/lessons/{lessonId}")]
        public async Task<IActionResult> DeleteEnrolment([FromRoute] int studentId, [FromRoute] int lessonId)
        {
            var enrolment = await _uow.Repository<Enrolment>().Find(studentId, lessonId);
            if (enrolment == null)
            {
                return NotFound();
            }

            _uow.Repository<Enrolment>().Remove(enrolment);
            if (!_uow.Complete(1))
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok();
        }
    }
}