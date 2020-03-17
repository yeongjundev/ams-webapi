using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs.AttendanceSheetDTOs
{
    public class PostAttendanceSheetDTO
    {
        [Required]
        public int LessonId { get; set; }

        [Required]
        public DateTime StartDateTime { get; set; }

        [Required]
        public DateTime EndDateTime { get; set; }
    }
}