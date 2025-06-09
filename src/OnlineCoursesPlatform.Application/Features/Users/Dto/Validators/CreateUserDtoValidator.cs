using FluentValidation;

namespace OnlineCoursesPlatform.Application.Features.Users.Dto.Validators
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("User name is required")
                .Length(2, 100).WithMessage("User name must be between 2 and 100 characters.");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email must be a valid email address.");
        }
    }
}
