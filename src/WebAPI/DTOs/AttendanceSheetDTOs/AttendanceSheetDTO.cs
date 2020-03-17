using System;
using System.Collections.Generic;
using WebAPI.DTOs.AttendanceLogDTOs;
using WebAPI.DTOs.LessonDTOs;

namespace WebAPI.DTOs.AttendanceSheetDTOs
{
    public class AttendanceSheetDTO
    {
        public int Id { get; set; }
        public double Duration { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }

        public SimpleLessonDTO Lesson { get; set; }
        public List<AttendanceLogWithOnlyStudentDTO> attendanceLogs { get; set; }
    }
}