using AutoMapper;
using OnlineCoursesPlatform.Application.Features.Auth.Dto;
using OnlineCoursesPlatform.Application.Features.Users.Dto;
using OnlineCoursesPlatform.Domain.Entities;

namespace OnlineCoursesPlatform.Application.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // Entity -> DTO
            CreateMap<User, UserDto>();
            //DTO -> Entity
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>();

            // Auth 
            CreateMap<RegisterDto, User>();
            CreateMap<CreateUserFromGoogleDto, User>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(_ => (string?)null))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(_ => (int?)null))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(_ => (string?)null));
        }
    }
}
