using System;
using System.Collections.Generic;
using WebAPI.DTOs.AttendanceSheetDTOs;
using WebAPI.DTOs.EnrolmentDTOs;

namespace WebAPI.DTOs.LessonDTOs
{
    public class DetailLessonDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }

        public List<EnrolmentWithStudentOnlyDTO> Enrolled { get; set; }
        public List<SimpleAttendanceSheetOnlyDTO> AttendanceSheets { get; set; }
    }
}