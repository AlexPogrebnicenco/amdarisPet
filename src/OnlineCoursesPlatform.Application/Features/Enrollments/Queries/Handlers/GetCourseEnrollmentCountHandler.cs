using MediatR;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;

namespace OnlineCoursesPlatform.Application.Features.Enrollments.Queries.Handlers
{
    public class GetCourseEnrollmentCountHandler : IRequestHandler<GetCourseEnrollmentCountQuery, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCourseEnrollmentCountHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(GetCourseEnrollmentCountQuery request, CancellationToken cancellationToken)
        {
            var course = await _unitOfWork.CourseRepository.GetByIdAsync(request.CourseId);

            if (course == null)
            {
                throw new Exception("Course not found.");
            }

            return course.Enrollments.Count;
        }
    }
}
