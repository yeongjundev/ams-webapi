using System;
using AutoMapper;
using Core.Entities;
using Infrastructure.Helpers;
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
        }
    }
}