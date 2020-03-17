using System.Collections.Generic;

namespace WebAPI.DTOs.StudentDTOs
{
    public class SimpleStudentsResultDTO
    {
        public int PageSize { get; set; }

        public int CurrentPage { get; set; }

        public int LastPage { get; set; }

        public int TotalCount { get; set; }

        public List<SimpleStudentDTO> Result { get; private set; }
    }
}