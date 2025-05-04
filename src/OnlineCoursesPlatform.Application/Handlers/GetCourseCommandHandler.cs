using MediatR;
using OnlineCoursesPlatform.Application.Queries;
using OnlineCoursesPlatform.Application.Repositories;
using OnlineCoursesPlatform.Domain.Models;

namespace OnlineCoursesPlatform.Application.Handlers
{
    public class GetCourseByIdQueryHandler : IRequestHandler<GetCourseByIdQuery, Course>
    {
        private readonly IRepository<Course> _repository;

        public GetCourseByIdQueryHandler(IRepository<Course> repository)
        {
            _repository = repository;
        }

        public Task<Course> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            var course = _repository.GetById(request.Id);
            return Task.FromResult(course);
        }
    }
}
