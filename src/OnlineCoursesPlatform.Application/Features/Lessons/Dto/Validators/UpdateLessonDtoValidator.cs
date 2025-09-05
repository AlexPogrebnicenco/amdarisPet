using FluentValidation;

namespace OnlineCoursesPlatform.Application.Features.Lessons.Dto.Validators
{
    public class UpdateLessonDtoValidator : AbstractValidator<UpdateLessonDto>
    {
        public UpdateLessonDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(4000).WithMessage("Description must not exceed 4000 characters.");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required.");

            RuleForEach(x => x.Resources)
                .Must(link => Uri.IsWellFormedUriString(link, UriKind.Absolute))
                .WithMessage("Each resource must be a valid URL.");

            RuleForEach(x => x.VideoUrls).ChildRules(video =>
            {
                video.RuleFor(v => v.Url)
                    .NotEmpty()
                    .Must(link => Uri.IsWellFormedUriString(link, UriKind.Absolute))
                    .WithMessage("Each video must have a valid URL.");

                video.RuleFor(v => v.Title)
                    .NotEmpty()
                    .WithMessage("Video title is required.");
            });
        }
    }
}
