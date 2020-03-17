using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebAPI.DTOs.LessonDTOs
{
    public class PutLessonDTO
    {
        [Required(AllowEmptyStrings = false)]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(300)]
        public string Description { get; set; }

        [JsonIgnore]
        public DateTime UpdateDateTime { get; set; } = DateTime.Now;
    }
}