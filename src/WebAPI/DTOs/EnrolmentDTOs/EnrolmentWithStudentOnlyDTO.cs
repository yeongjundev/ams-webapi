using System;
using WebAPI.DTOs.StudentDTOs;

namespace WebAPI.DTOs.EnrolmentDTOs
{
    public class EnrolmentWithStudentOnlyDTO
    {
        public SimpleStudentDTO Student { get; set; }

        public DateTime CreateDateTime { get; set; }
    }
}