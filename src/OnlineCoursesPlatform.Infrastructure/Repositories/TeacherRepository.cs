using Microsoft.EntityFrameworkCore;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Domain.Entities;
using OnlineCoursesPlatform.Infrastructure.Persistence;

namespace OnlineCoursesPlatform.Infrastructure.Repositories
{
    public class TeacherRepository : RepositoryEF<Teacher>, ITeacherRepository
    {
        public TeacherRepository(AppDbContext context) : base(context)
        {
        }
    }
}
