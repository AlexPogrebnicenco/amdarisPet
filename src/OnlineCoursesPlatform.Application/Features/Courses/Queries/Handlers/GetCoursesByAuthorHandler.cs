using AutoMapper;
using MediatR;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Application.Common.Dto;
using OnlineCoursesPlatform.Application.Features.Courses.Dto;
using OnlineCoursesPlatform.Application.Features.Courses.Queries;

public class GetCoursesByAuthorHandler : IRequestHandler<GetCoursesByAuthorQuery, PagedResult<CourseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCoursesByAuthorHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PagedResult<CourseDto>> Handle(GetCoursesByAuthorQuery request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.CourseRepository.QueryCoursesByAuthorId(request.AuthorId);

        // Search
        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.ToLower();
            query = query.Where(c => c.Title.ToLower().Contains(search));
        }

        // Filert by Tag
        if (!string.IsNullOrWhiteSpace(request.Tag))
        {
            query = query.Where(c =>
                c.CourseTags.Any(ct => ct.Tag != null && ct.Tag.Name == request.Tag));
        }

        // Sort
        query = request.Sort switch
        {
            "lastModified" => query.OrderByDescending(c => c.DateModified),
            "mostPopular" => query.OrderByDescending(c => c.Enrollments.Count),
            "longest" => query.OrderByDescending(c => c.Lessons.Count),
            "shortest" => query.OrderBy(c => c.Lessons.Count),
            _ => query.OrderByDescending(c => c.DateCreated) // default: lastCreated
        };

        // Pagination
        var totalCount = await _unitOfWork.CourseRepository.CountTeacherCoursesAsync(query, cancellationToken);
        var pagedCourses = await _unitOfWork.CourseRepository.GetPagedTeacherCoursesAsync(query, request.PageNumber, request.PageSize, cancellationToken);

        var mapped = _mapper.Map<List<CourseDto>>(pagedCourses);

        return new PagedResult<CourseDto>
        {
            Items = mapped,
            TotalCount = totalCount
        };
    }
}