using AutoMapper;
using OnlineCoursesPlatform.Application.Features.Teachers.Dto;
using OnlineCoursesPlatform.Domain.Entities;

namespace OnlineCoursesPlatform.Application.Mapping
{
    public class TeacherProfile : Profile
    {
        public TeacherProfile() 
        {
            //Entity -> DTO
            CreateMap<Teacher, TeacherDto>();
            //DTO -> Entity
            CreateMap<CreateTeacherDto, Teacher>();
            CreateMap<UpdateTeacherDto, Teacher>();
        }
    }
}
