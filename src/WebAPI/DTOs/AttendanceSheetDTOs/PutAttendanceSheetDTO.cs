using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs.AttendanceSheetDTOs
{
    public class PutAttendanceSheetDTO
    {
        [Required]
        public DateTime StartDateTime { get; set; }

        [Required]
        public DateTime EndDateTime { get; set; }
    }
}