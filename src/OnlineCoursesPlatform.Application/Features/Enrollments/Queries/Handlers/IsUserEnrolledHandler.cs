using MediatR;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;

namespace OnlineCoursesPlatform.Application.Features.Enrollments.Queries.Handlers
{
    public class IsUserEnrolledHandler : IRequestHandler<IsUserEnrolledQuery, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public IsUserEnrolledHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(IsUserEnrolledQuery request, CancellationToken cancellationToken)
        {
            var enrollment = await _unitOfWork.EnrollmentRepository
                .GetByUserIdAndCourseIdAsync(request.UserId, request.CourseId);

            return enrollment != null;
        }
    }
}
