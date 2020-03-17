using System;
using AutoMapper;
using Core.Entities;
using Core.Enums;
using Infrastructure.Helpers;
using WebAPI.DTOs.AttendanceLogDTOs;
using WebAPI.DTOs.AttendanceSheetDTOs;
using WebAPI.DTOs.EnrolmentDTOs;
using WebAPI.DTOs.LessonDTOs;
using WebAPI.DTOs.StudentDTOs;

namespace WebAPI.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Student Mapping
            CreateMap<Student, SimpleStudentDTO>();
            CreateMap<QueryResultObject<Student>, SimpleStudentsResultDTO>()
                .ForMember(dest => dest.Result, opt => opt.MapFrom(src => src.QueryResult));
            CreateMap<Student, DetailStudentDTO>()
                .ForMember(dest => dest.Enrolled, opt => opt.MapFrom(src => src.Enrolments));
            CreateMap<PostStudentDTO, Student>()
                .ForMember(dest => dest.UpdateDateTime, opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<PutStudentDTO, Student>()
                .ForMember(dest => dest.Id, opt => opt.UseDestinationValue())
                .ForMember(dest => dest.CreateDateTime, opt => opt.UseDestinationValue())
                .ForMember(dest => dest.UpdateDateTime, opt => opt.MapFrom(src => DateTime.Now));

            // Lesson Mapping
            CreateMap<Lesson, SimpleLessonDTO>();
            CreateMap<QueryResultObject<Lesson>, SimpleLessonsResultDTO>()
                .ForMember(dest => dest.Result, opt => opt.MapFrom(src => src.QueryResult));
            CreateMap<Lesson, DetailLessonDTO>()
                .ForMember(dest => dest.Enrolled, opt => opt.MapFrom(src => src.Enrolments));
            CreateMap<PostLessonDTO, Lesson>()
                .ForMember(dest => dest.UpdateDateTime, opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<PutLessonDTO, Lesson>()
                .ForMember(dest => dest.Id, opt => opt.UseDestinationValue())
                .ForMember(dest => dest.CreateDateTime, opt => opt.UseDestinationValue())
                .ForMember(dest => dest.UpdateDateTime, opt => opt.MapFrom(src => DateTime.Now));

            // Enrolment Mapping
            CreateMap<Enrolment, EnrolmentDTO>();
            CreateMap<QueryResultObject<Enrolment>, EnrolmentsResultDTO>()
                .ForMember(dest => dest.Result, opt => opt.MapFrom(src => src.QueryResult));
            CreateMap<Enrolment, EnrolmentWithLessonOnlyDTO>();
            CreateMap<Enrolment, EnrolmentWithStudentOnlyDTO>();

            // AttendanceSheet Mapping
            CreateMap<AttendanceSheet, SimpleAttendanceSheetDTO>()
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration.TotalHours));
            CreateMap<AttendanceSheet, SimpleAttendanceSheetOnlyDTO>()
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration.TotalHours));
            CreateMap<QueryResultObject<AttendanceSheet>, SimpleAttendanceSheetsResultDTO>()
                .ForMember(dest => dest.Result, opt => opt.MapFrom(src => src.QueryResult));
            CreateMap<PostAttendanceSheetDTO, AttendanceSheet>()
                .ForMember(dest => dest.UpdateDateTime, opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<AttendanceSheet, AttendanceSheetDTO>()
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration.TotalHours));
            CreateMap<PutAttendanceSheetDTO, AttendanceSheet>()
                .ForMember(dest => dest.Id, opt => opt.UseDestinationValue())
                .ForMember(dest => dest.CreateDateTime, opt => opt.UseDestinationValue())
                .ForMember(dest => dest.UpdateDateTime, opt => opt.MapFrom(src => DateTime.Now));

            // AttendanceLog Mapping
            CreateMap<AttendanceLog, AttendanceLogDTO>()
                .ForMember(dest => dest.Attendance, opt => opt.MapFrom(src => src.Attendance.ToString()));
            CreateMap<AttendanceLog, SimpleAttendanceLogOnlyDTO>()
                .ForMember(dest => dest.Attendance, opt => opt.MapFrom(src => src.Attendance.ToString()));
            CreateMap<AttendanceLog, AttendanceLogWithOnlyStudentDTO>()
                .ForMember(dest => dest.Attendance, opt => opt.MapFrom(src => src.Attendance.ToString()));
            CreateMap<AttendanceLog, AttendanceLogWithLessonAndAttendanceSheetOnlyDTO>()
                .ForMember(dest => dest.Attendance, opt => opt.MapFrom(src => src.Attendance.ToString()));
            CreateMap<PutAttendanceLogDTO, AttendanceLog>()
                .ForMember(dest => dest.Attendance, opt => opt.MapFrom(src => StringToAttendanceEnum(src.Attendance)))
                .ForMember(dest => dest.LessonId, opt => opt.UseDestinationValue())
                .ForMember(dest => dest.StudentId, opt => opt.UseDestinationValue())
                .ForMember(dest => dest.AttendanceSheetId, opt => opt.UseDestinationValue())
                .ForMember(dest => dest.CreateDateTime, opt => opt.UseDestinationValue())
                .ForMember(dest => dest.UpdateDateTime, opt => opt.MapFrom(src => DateTime.Now));
        }

        private Attendance StringToAttendanceEnum(string str)
        {
            Attendance attendance;
            var result = Enum.TryParse<Attendance>(str, true, out attendance);

            return result ? attendance : Attendance.trial;
        }
    }
}