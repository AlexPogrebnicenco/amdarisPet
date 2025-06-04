using AutoMapper;
using OnlineCoursesPlatform.Application.Features.Users.Commands;
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
            //CreateUser -> Entity
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>();
        }
    }
}
