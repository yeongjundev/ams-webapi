using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Student : Entity
    {
        [Key]
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }

        public List<Enrolment> Enrolments { get; set; }

        public List<AttendanceLog> AttendanceLogs { get; set; }

        public Student() : base() { }
    }
}