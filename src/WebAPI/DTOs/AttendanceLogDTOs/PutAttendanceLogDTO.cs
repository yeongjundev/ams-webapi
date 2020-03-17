using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs.AttendanceLogDTOs
{
    public class PutAttendanceLogDTO
    {
        [Required]
        public string Attendance { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string Comment { get; set; }
    }
}