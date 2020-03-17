using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebAPI.DTOs.StudentDTOs
{
    public class PutStudentDTO
    {
        [Required(AllowEmptyStrings = false)]
        public string Firstname { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string Middlename { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Lastname { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string Phone { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(300)]
        public string Description { get; set; }

        [JsonIgnore]
        public DateTime UpdateDateTime { get; set; } = DateTime.Now;
    }
}