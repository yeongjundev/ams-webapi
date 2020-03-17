using System;
using WebAPI.DTOs.AttendanceSheetDTOs;
using WebAPI.DTOs.LessonDTOs;
using WebAPI.DTOs.StudentDTOs;

namespace WebAPI.DTOs.AttendanceLogDTOs
{
    public class SimpleAttendanceLogOnlyDTO
    {
        public string Attendance { get; set; }
        public string Comment { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}