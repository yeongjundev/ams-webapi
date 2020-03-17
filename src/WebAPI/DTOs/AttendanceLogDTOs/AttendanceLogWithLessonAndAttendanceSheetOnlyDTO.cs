using System;
using WebAPI.DTOs.AttendanceSheetDTOs;
using WebAPI.DTOs.LessonDTOs;

namespace WebAPI.DTOs.AttendanceLogDTOs
{
    public class AttendanceLogWithLessonAndAttendanceSheetOnlyDTO
    {
        public SimpleAttendanceSheetOnlyDTO AttendanceSheet { get; set; }
        public SimpleLessonDTO Lesson { get; set; }
        public string Attendance { get; set; }
        public string Comment { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}