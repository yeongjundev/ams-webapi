using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Enrolment : Entity
    {
        [Key, Column(Order = 0)]
        public int StudentId { get; set; }

        [Key, Column(Order = 1)]
        public int LessonId { get; set; }

        [ForeignKey("StudentId")]
        public Student Student { get; set; }

        [ForeignKey("LessonId")]
        public Lesson Lesson { get; set; }
    }
}