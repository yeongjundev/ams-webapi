using System;
using WebAPI.DTOs.LessonDTOs;
using WebAPI.DTOs.StudentDTOs;

namespace WebAPI.DTOs.EnrolmentDTOs
{
    public class EnrolmentWithLessonOnlyDTO
    {
        public SimpleLessonDTO Lesson { get; set; }

        public DateTime CreateDateTime { get; set; }
    }
}