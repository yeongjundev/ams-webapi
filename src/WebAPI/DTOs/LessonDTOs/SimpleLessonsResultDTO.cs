using System.Collections.Generic;

namespace WebAPI.DTOs.LessonDTOs
{
    public class SimpleLessonsResultDTO
    {
        public int PageSize { get; set; }

        public int CurrentPage { get; set; }

        public int LastPage { get; set; }

        public int TotalCount { get; set; }

        public List<SimpleLessonDTO> Result { get; set; }
    }
}