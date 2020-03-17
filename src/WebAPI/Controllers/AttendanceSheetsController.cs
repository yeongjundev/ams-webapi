using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities;
using Core.Specifications.AttendanceSheetSpecifications;
using Core.Specifications.LessonSpecifications;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAPI.DTOs.AttendanceSheetDTOs;
using WebAPI.Helpers;

namespace WebAPI.Controllers
{
    public class AttendanceSheetsController : ApiControllerBase<AttendanceSheetsController>
    {
        public AttendanceSheetsController(ILogger<AttendanceSheetsController> logger, IMapper mapper, IUnitOfWork uow) : base(logger, mapper, uow) { }

        // api/attendancesheets/
        [HttpPost]
        public async Task<IActionResult> PostAttendanceSheet([FromBody] PostAttendanceSheetDTO body)
        {
            var qro = await _uow.Repository<Lesson>().Find(new DetailLessonSpecification(body.LessonId));
            if (qro == null || qro.QueryResult == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var lesson = qro.QueryResult.SingleOrDefault();
            if (lesson == null)
            {
                return NotFound();
            }

            // Create AttendanceSheet
            var newAttendanceSheet = _mapper.Map<AttendanceSheet>(body);
            newAttendanceSheet.Lesson = lesson;
            _uow.Repository<AttendanceSheet>().Add(newAttendanceSheet);
            if (!_uow.Complete(1))
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            // Create AttendanceLog for enrolled students
            var newAttendanceLogs = new List<AttendanceLog>();
            foreach (var enrolment in lesson.Enrolments)
            {
                var newLog = new AttendanceLog(enrolment.Student.Id, lesson.Id, newAttendanceSheet.Id);
                newLog.Student = enrolment.Student;
                newAttendanceLogs.Add(newLog);
            }
            newAttendanceSheet.AttendanceLogs = newAttendanceLogs;

            _uow.Repository<AttendanceSheet>().Update(newAttendanceSheet);
            _uow.Repository<AttendanceLog>().AddRange(newAttendanceLogs);
            if (!_uow.Complete(newAttendanceLogs.Count))
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(_mapper.Map<AttendanceSheetDTO>(newAttendanceSheet));
        }

        // api/attendanceSheets/{id:int}
        [HttpGet("{id}", Name = "GetAttendanceSheet")]
        public async Task<IActionResult> GetAttendanceSheet([FromRoute] int id)
        {
            var qro = await _uow.Repository<AttendanceSheet>().Find(new DetailAttendanceSheetSpecification(id));
            if (qro == null || qro.QueryResult == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var attendanceSheet = qro.QueryResult.SingleOrDefault();
            if (attendanceSheet == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<AttendanceSheetDTO>(attendanceSheet));
        }

        // api/attendanceSheets/
        [HttpGet]
        public async Task<IActionResult> GetAttendanceSheets(
            [FromQuery] OrderingOption orderingOption,
            [FromQuery] PagingOption pagingOption
        )
        {
            var specification = new SimpleAttendanceSheetsSpecification(
                    orderingOption.GetOrderByInfos(),
                    pagingOption.CurrentPage, pagingOption.PageSize
                );
            var qro = await _uow.Repository<AttendanceSheet>().Find(specification);
            if (qro == null || qro.QueryResult == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(_mapper.Map<SimpleAttendanceSheetsResultDTO>(qro));
        }

        // api/attendanceSheets/{id:int}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttendanceSheet([FromRoute] int id, [FromBody] PutAttendanceSheetDTO body)
        {
            var attendanceSheet = await _uow.Repository<AttendanceSheet>().Find(id);
            if (attendanceSheet == null)
            {
                return NotFound();
            }

            attendanceSheet = _mapper.Map(body, attendanceSheet);
            _uow.Repository<AttendanceSheet>().Update(attendanceSheet);

            if (!_uow.Complete(1))
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(_mapper.Map<SimpleAttendanceSheetOnlyDTO>(attendanceSheet));
        }

        // api/attendanceSheets/{id:int}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttendanceSheet([FromRoute] int id)
        {
            var attendanceSheet = await _uow.Repository<AttendanceSheet>().Find(id);
            if (attendanceSheet == null)
            {
                return NotFound();
            }

            _uow.Repository<AttendanceSheet>().Remove(attendanceSheet);
            if (!_uow.Complete(1))
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok();
        }
    }
}