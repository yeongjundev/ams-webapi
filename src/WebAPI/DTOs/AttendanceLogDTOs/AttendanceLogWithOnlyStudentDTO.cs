using System;
using WebAPI.DTOs.StudentDTOs;

namespace WebAPI.DTOs.AttendanceLogDTOs
{
    public class AttendanceLogWithOnlyStudentDTO
    {
        public SimpleStudentDTO Student { get; set; }
        public string Attendance { get; set; }
        public string Comment { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}