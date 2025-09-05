using FluentValidation;

namespace OnlineCoursesPlatform.Application.Features.Courses.Dto.Validators
{
    public class UpdateCourseDtoValidator : AbstractValidator<UpdateCourseDto>
    {
        public UpdateCourseDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(4000).WithMessage("Description must not exceed 4000 characters.");

            RuleFor(x => x.Difficulty)
                .IsInEnum()
                .When(x => x.Difficulty.HasValue)
                .WithMessage("Difficulty must be one of: Beginner, Intermediate, Advanced.");
            RuleFor(x => x.About)
                .MaximumLength(4000)
                .WithMessage("About must not exceed 4000 characters.")
                .When(x => x.About != null);
        }
    }
}
