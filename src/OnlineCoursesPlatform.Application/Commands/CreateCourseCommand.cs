using MediatR;
using OnlineCoursesPlatform.Domain.Models;

namespace OnlineCoursesPlatform.Application.Commands
{
    public class CreateCourseCommand : IRequest<Course>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Teacher { get; set; }

        public CreateCourseCommand(string title, string description, string teacher)
        {
            Title = title;
            Description = description;
            Teacher = teacher;
        }
    }
}
