using System;
using AutoMapper;
using Core.Entities;
using WebAPI.DTOs;
using WebAPI.DTOs.StudentDTOs;

namespace WebAPI.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Student Mapping
            CreateMap<Student, StudentOnlyDTO>();
            CreateMap<PostStudentDTO, Student>()
                .ForMember(dest => dest.CreateDateTime, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.UpdateDateTime, opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<PutStudentDTO, Student>()
                .ForMember(dest => dest.Id, opt => opt.UseDestinationValue())
                .ForMember(dest => dest.CreateDateTime, opt => opt.UseDestinationValue())
                .ForMember(dest => dest.UpdateDateTime, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}