using AutoMapper;
using OnlineCoursesPlatform.Application.Features.TeacherRegistrationRequests.Dto;
using OnlineCoursesPlatform.Domain.Entities;

namespace OnlineCoursesPlatform.Application.Mapping
{
    public class TeacherRegistrationRequestProfile : Profile
    {
        public TeacherRegistrationRequestProfile()
        {
            // Маппинг TeacherRegistrationRequest -> TeacherRequestDto (для выдачи на фронт)
            CreateMap<TeacherRegistrationRequest, TeacherRequestDto>();

            // Маппинг TeacherRegistrationRequest -> User (для создания нового пользователя)
            CreateMap<TeacherRegistrationRequest, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) //  Игнорируем Id
                .ForMember(dest => dest.Role, opt => opt.MapFrom(_ => "Teacher")) 
                .ForMember(dest => dest.IsApproved, opt => opt.MapFrom(_ => true)); 
        }
    }
}
