using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebAPI.DTOs.EnrolmentDTOs
{
    public class PostEnrolmentDTO
    {
        [Required]
        public int StudentId { get; set; }

        [Required]
        public int LessonId { get; set; }

        [JsonIgnore]
        public DateTime CreateDateTime { get; set; } = DateTime.Now;

        [JsonIgnore]
        public DateTime UpdateDateTime { get; set; } = DateTime.Now;
    }
}