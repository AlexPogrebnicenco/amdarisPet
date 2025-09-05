using FluentValidation;
using OnlineCoursesPlatform.Application.Features.Users.Dto;

namespace OnlineCoursesPlatform.Application.Features.Users.Dto.Validators
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("User name is required.")
                .Length(2, 100).WithMessage("User name must be between 2 and 100 characters.");

            RuleFor(x => x.AvatarUrl)
                .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
                .When(x => !string.IsNullOrWhiteSpace(x.AvatarUrl))
                .WithMessage("Avatar URL must be a valid URL.");
        }
    }
}
