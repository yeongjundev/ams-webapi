using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs.AttendanceLogDTOs
{
    public class PostAttendanceLogDTO
    {
        [Required]
        public int StudentId { get; set; }

        [Required]
        public int AttendanceSheetId { get; set; }
    }
}