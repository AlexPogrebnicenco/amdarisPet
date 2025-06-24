using FluentValidation;

namespace OnlineCoursesPlatform.Application.Features.TeacherRegistrationRequests.Dto.Validators
{
    public class CreateTeacherRegistrationRequestDtoValidator : AbstractValidator<CreateTeacherRegistrationRequestDto>
    {

        public CreateTeacherRegistrationRequestDtoValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("User name is required.")
                .Length(2, 100).WithMessage("User name must be between 2 and 100 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.Age)
                .GreaterThanOrEqualTo(13).WithMessage("You must be at least 13 years old.");

            RuleFor(x => x.Gender)
                .NotEmpty().WithMessage("Gender is required.");
        }
    }
}
