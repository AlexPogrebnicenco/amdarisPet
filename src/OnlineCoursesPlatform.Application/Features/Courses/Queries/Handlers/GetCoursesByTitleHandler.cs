using AutoMapper;
using MediatR;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Application.Features.Courses.Dto;
using OnlineCoursesPlatform.Application.Features.Courses.Queries;

public class GetCoursesByTitleHandler : IRequestHandler<GetCoursesByTitleQuery, IEnumerable<CourseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCoursesByTitleHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CourseDto>> Handle(GetCoursesByTitleQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Query) || request.Query.Length < 3)
            return Enumerable.Empty<CourseDto>();

        var courses = await _unitOfWork.CourseRepository
            .SearchByTitleAsync(request.Query, request.PageNumber, request.PageSize);

        return courses; 
    }
}
