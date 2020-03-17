using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Lesson : Entity
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public List<Enrolment> Enrolments { get; set; }

        public List<AttendanceSheet> AttendanceSheets { get; set; }

        public Lesson() : base() { }
    }
}