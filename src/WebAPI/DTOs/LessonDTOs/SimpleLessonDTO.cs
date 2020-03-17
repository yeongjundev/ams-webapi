using System;

namespace WebAPI.DTOs.LessonDTOs
{
    public class SimpleLessonDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}