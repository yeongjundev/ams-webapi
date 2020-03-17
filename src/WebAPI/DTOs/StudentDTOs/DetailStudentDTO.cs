using System;
using System.Collections.Generic;
using WebAPI.DTOs.EnrolmentDTOs;

namespace WebAPI.DTOs.StudentDTOs
{
    public class DetailStudentDTO
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }

        public List<EnrolmentWithLessonOnlyDTO> Enrolled { get; set; }
    }
}