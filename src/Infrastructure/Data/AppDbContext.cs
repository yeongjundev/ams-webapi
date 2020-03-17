using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<Lesson> Lessons { get; set; }

        public DbSet<Enrolment> Enrolments { get; set; }

        public DbSet<AttendanceSheet> AttendanceSheets { get; set; }

        public DbSet<AttendanceLog> AttendanceLogs { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Enrolment
            builder.Entity<Enrolment>()
                .HasKey(enrolment => new { enrolment.StudentId, enrolment.LessonId });
            builder.Entity<Enrolment>()
                .HasOne(enrolment => enrolment.Student)
                .WithMany(student => student.Enrolments)
                .HasForeignKey(enrolment => enrolment.StudentId);
            builder.Entity<Enrolment>()
                .HasOne(enrolment => enrolment.Lesson)
                .WithMany(lesson => lesson.Enrolments)
                .HasForeignKey(enrolment => enrolment.LessonId);

            // AttendanceLog
            builder.Entity<AttendanceLog>()
                .HasKey(attendance => new { attendance.StudentId, attendance.LessonId, attendance.AttendanceSheetId });
            builder.Entity<AttendanceLog>()
                .HasOne(attendance => attendance.Student)
                .WithMany(student => student.AttendanceLogs)
                .HasForeignKey(attendance => attendance.StudentId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<AttendanceLog>()
                .HasOne(attendance => attendance.Lesson)
                .WithMany()
                .HasForeignKey(attendance => attendance.LessonId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<AttendanceLog>()
                .HasOne(attendance => attendance.AttendanceSheet)
                .WithMany(attendanceSheet => attendanceSheet.AttendanceLogs)
                .HasForeignKey(attendance => attendance.AttendanceSheetId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}