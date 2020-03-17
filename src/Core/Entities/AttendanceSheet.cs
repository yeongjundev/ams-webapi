using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class AttendanceSheet : Entity
    {
        [Key]
        public int Id { get; set; }
        public int LessonId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public TimeSpan Duration
        {
            get
            {
                return EndDateTime - StartDateTime;
            }
        }

        [ForeignKey("LessonId")]
        public Lesson Lesson { get; set; }

        public List<AttendanceLog> AttendanceLogs { get; set; }

        public AttendanceSheet() : base() { }

        public AttendanceSheet(int lessonId, DateTime startDateTime, DateTime endDateTime) : base()
        {
            LessonId = lessonId;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
        }
    }
}