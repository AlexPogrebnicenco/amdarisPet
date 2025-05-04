using MediatR;
using OnlineCoursesPlatform.Domain.Models;

namespace OnlineCoursesPlatform.Application.Queries
{
    public class GetCourseByIdQuery : IRequest<Course>
    {
        public int Id { get; set; }

        public GetCourseByIdQuery(int id)
        {
            Id = id;
        }
    }
}
