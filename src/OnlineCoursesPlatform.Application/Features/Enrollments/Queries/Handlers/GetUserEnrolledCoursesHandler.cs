using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Application.Common.Dto;
using OnlineCoursesPlatform.Application.Features.Courses.Dto;

namespace OnlineCoursesPlatform.Application.Features.Enrollments.Queries.Handlers;

public class GetUserEnrolledCoursesHandler : IRequestHandler<GetUserEnrolledCoursesQuery, PagedResult<CourseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<GetUserEnrolledCoursesHandler> _logger;

    public GetUserEnrolledCoursesHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<GetUserEnrolledCoursesHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PagedResult<CourseDto>> Handle(GetUserEnrolledCoursesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching enrolled courses for userId={UserId}, page={Page}, size={Size}, sort={Sort}, tag={Tag}, search={Search}",
            request.UserId, request.PageNumber, request.PageSize, request.Sort, request.Tag, request.Search);

        // Get base query
        var query = _unitOfWork.EnrollmentRepository.QueryCoursesByUserId(request.UserId);

        // Apply search filter
        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            query = query.Where(c =>
                c.Title.ToLower().Contains(request.Search.ToLower()));
        }

        // Apply tag filter
        if (!string.IsNullOrWhiteSpace(request.Tag))
        {
            query = query.Where(c =>
                c.CourseTags.Any(ct => ct.Tag != null && ct.Tag.Name == request.Tag));
        }

        // Apply sorting
        query = request.Sort switch
        {
            "lastModified" => query.OrderByDescending(c => c.DateModified),
            "mostPopular" => query.OrderByDescending(c => c.Enrollments.Count),
            "longest" => query.OrderByDescending(c => c.Lessons.Count),
            "shortest" => query.OrderBy(c => c.Lessons.Count),
            _ => query.OrderByDescending(c => c.DateCreated) // default: lastCreated
        };

        // Get total count
        var totalCount = await _unitOfWork.EnrollmentRepository.CountUserEnrolledCoursesAsync(query, cancellationToken);


        // Apply pagination
        var pagedCourses = await _unitOfWork.EnrollmentRepository
             .GetPagedUserEnrolledCoursesAsync(query, request.PageNumber, request.PageSize, cancellationToken);

        // Map to DTO
        var mapped = _mapper.Map<List<CourseDto>>(pagedCourses);

        return new PagedResult<CourseDto>
        {
            Items = mapped,
            TotalCount = totalCount
        };
    }
}
