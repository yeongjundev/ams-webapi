using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Enums;

namespace Core.Entities
{
    public class AttendanceLog : Entity
    {
        [Key, Column(Order = 0)]
        public int StudentId { get; set; }

        [Key, Column(Order = 1)]
        public int LessonId { get; set; }

        [Key, Column(Order = 2)]
        public int AttendanceSheetId { get; set; }

        public Attendance Attendance { get; set; } = Attendance.Absent;
        public string Comment { get; set; }

        [ForeignKey("StudentId")]
        public Student Student { get; set; }

        [ForeignKey("LessonId")]
        public Lesson Lesson { get; set; }

        [ForeignKey("AttendanceSheetId")]
        public AttendanceSheet AttendanceSheet { get; set; }

        public AttendanceLog() : base() { }

        public AttendanceLog(int studentId, int lessonId, int attendanceSheetId) : base()
        {
            StudentId = studentId;
            LessonId = lessonId;
            AttendanceSheetId = attendanceSheetId;
            Comment = "";
        }
    }
}