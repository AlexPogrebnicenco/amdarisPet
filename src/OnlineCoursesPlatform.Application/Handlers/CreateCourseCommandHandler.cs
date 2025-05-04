using MediatR;
using OnlineCoursesPlatform.Application.Commands;
using OnlineCoursesPlatform.Application.Repositories;
using OnlineCoursesPlatform.Domain.Models;

namespace OnlineCoursesPlatform.Application.Handlers
{
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, Course>
    {
        private readonly IRepository<Course> _repository;

        public CreateCourseCommandHandler(IRepository<Course> repository)
        {
            _repository = repository;
        }

        public Task<Course> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = new Course(0, request.Title, request.Description, request.Teacher);
            _repository.Add(course);
            return Task.FromResult(course);
        }
    }
}
