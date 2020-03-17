using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities;
using Core.Enums;
using Core.Specifications.AttendanceSheetSpecifications;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAPI.DTOs.AttendanceLogDTOs;
using WebAPI.Helpers;

namespace WebAPI.Controllers
{
    public class AttendanceLogsController : ApiControllerBase<AttendanceLogsController>
    {
        public AttendanceLogsController(ILogger<AttendanceLogsController> logger, IMapper mapper, IUnitOfWork uow) : base(logger, mapper, uow) { }

        // api/attendancelogs/
        [HttpPost]
        public async Task<IActionResult> PostAttendanceLog([FromBody] PostAttendanceLogDTO body)
        {
            var getAttendanceSheetQro = _uow.Repository<AttendanceSheet>().Find(new AttendanceSheetWithLessonSpecification(body.AttendanceSheetId));
            var getStudent = _uow.Repository<Student>().Find(body.StudentId);

            if (await getAttendanceSheetQro == null || getAttendanceSheetQro.Result.QueryResult == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var attendanceSheet = getAttendanceSheetQro.Result.QueryResult.SingleOrDefault();
            var student = getStudent.Result;
            if (attendanceSheet == null || student == null)
            {
                return NotFound();
            }

            // Create AttendanceLog
            var newAttendanceLog = new AttendanceLog
            {
                StudentId = student.Id,
                Student = student,
                LessonId = attendanceSheet.LessonId,
                Lesson = attendanceSheet.Lesson,
                AttendanceSheetId = attendanceSheet.Id,
                AttendanceSheet = attendanceSheet,
                Comment = "",
                Attendance = Attendance.trial
            };
            _uow.Repository<AttendanceLog>().Add(newAttendanceLog);

            if (!_uow.Complete(1))
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return CreatedAtRoute(
                "GetAttendanceLog",
                new { newAttendanceLog.AttendanceSheetId, newAttendanceLog.StudentId },
                _mapper.Map<AttendanceLogDTO>(newAttendanceLog)
            );
        }

        // api/attendancelogs/attendanceSheets/{attendanceSheetId:int}/students/{studentId:int}
        [HttpGet("attendanceSheets/{attendanceSheetId}/students/{studentId}", Name = "GetAttendanceLog")]
        public async Task<IActionResult> GetAttendanceLog([FromRoute] int attendanceSheetId, [FromRoute] int studentId)
        {
            var getAttendanceSheetQro = _uow.Repository<AttendanceSheet>().Find(new AttendanceSheetWithLessonSpecification(attendanceSheetId));
            var getStudent = _uow.Repository<Student>().Find(studentId);

            if (await getAttendanceSheetQro == null || getAttendanceSheetQro.Result.QueryResult == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var attendanceSheet = getAttendanceSheetQro.Result.QueryResult.SingleOrDefault();
            var student = getStudent.Result;
            if (attendanceSheet == null || student == null)
            {
                return NotFound();
            }

            var attendanceLog = _uow.Repository<AttendanceLog>().Find(studentId, attendanceSheetId);
            if (attendanceLog == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<AttendanceLogDTO>(attendanceLog));
        }

        // api/attendancelogs/attendanceSheets/{attendanceSheetId:int}/students/{studentId:int}
        [HttpPut("attendanceSheets/{attendanceSheetId}/students/{studentId}")]
        public async Task<IActionResult> PutAttendanceLog(
            [FromRoute] int attendanceSheetId,
            [FromRoute] int studentId,
            [FromBody] PutAttendanceLogDTO body)
        {
            var attendanceLog = await _uow.Repository<AttendanceLog>().Find(studentId, attendanceSheetId);
            if (attendanceLog == null)
            {
                return NotFound();
            }

            attendanceLog = _mapper.Map(body, attendanceLog);
            _uow.Repository<AttendanceLog>().Update(attendanceLog);

            if (!_uow.Complete(1))
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(_mapper.Map<SimpleAttendanceLogOnlyDTO>(attendanceLog));
        }

        // api/attendancelogs/attendanceSheets/{attendanceSheetId:int}/students/{studentId:int}
        [HttpDelete("attendanceSheets/{attendanceSheetId}/students/{studentId}")]
        public async Task<IActionResult> DeleteAttendanceLog(
            [FromRoute] int attendanceSheetId,
            [FromRoute] int studentId)
        {
            var attendanceLog = await _uow.Repository<AttendanceLog>().Find(studentId, attendanceSheetId);
            if (attendanceLog == null)
            {
                return NotFound();
            }

            _uow.Repository<AttendanceLog>().Remove(attendanceLog);
            if (!_uow.Complete(1))
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok();
        }
    }
}