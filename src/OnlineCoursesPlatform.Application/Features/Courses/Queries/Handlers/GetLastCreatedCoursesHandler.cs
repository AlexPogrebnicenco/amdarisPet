using AutoMapper;
using MediatR;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Application.Features.Courses.Dto;

namespace OnlineCoursesPlatform.Application.Features.Courses.Queries.Handlers
{
    public class GetLastCreatedCoursesHandler : IRequestHandler<GetLastCreatedCoursesQuery, IEnumerable<CourseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetLastCreatedCoursesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CourseDto>> Handle(GetLastCreatedCoursesQuery request, CancellationToken cancellationToken)
        {
            var courses = await _unitOfWork.CourseRepository.GetLastCreatedAsync(request.PageNumber, request.PageSize);
            return _mapper.Map<IEnumerable<CourseDto>>(courses);
        }
    }

}
