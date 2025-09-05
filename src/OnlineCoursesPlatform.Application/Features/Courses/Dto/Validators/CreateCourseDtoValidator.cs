using FluentValidation;
using OnlineCoursesPlatform.Application.Features.Courses.Dto;

namespace OnlineCoursesPlatform.Application.Features.Courses.Validators
{
    public class CreateCourseDtoValidator : AbstractValidator<CreateCourseDto>
    {
        public CreateCourseDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(4000).WithMessage("Description must not exceed 4000 characters.");

            RuleFor(x => x.TagIds)
            .NotEmpty().WithMessage("At least one tag must be selected.");

            RuleForEach(x => x.TagIds)
                 .GreaterThan(0).WithMessage("Tag ID must be greater than 0.");

            RuleFor(x => x.Difficulty)
                 .IsInEnum().WithMessage("Difficulty must be one of: Beginner, Intermediate, Advanced.");
            RuleFor(x => x.About)
                 .MaximumLength(4000)
                 .WithMessage("About must not exceed 4000 characters.");
        }
    }
}
