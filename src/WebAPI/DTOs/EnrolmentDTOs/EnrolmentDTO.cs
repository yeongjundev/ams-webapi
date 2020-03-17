using System;
using WebAPI.DTOs.LessonDTOs;
using WebAPI.DTOs.StudentDTOs;

namespace WebAPI.DTOs.EnrolmentDTOs
{
    public class EnrolmentDTO
    {
        public SimpleLessonDTO Lesson { get; set; }

        public SimpleStudentDTO Student { get; set; }

        public DateTime CreateDateTime { get; set; }
    }
}