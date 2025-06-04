using Microsoft.EntityFrameworkCore;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Domain.Entities;
using OnlineCoursesPlatform.Infrastructure.Persistence;

namespace OnlineCoursesPlatform.Infrastructure.Repositories
{
    public class ProgressRecordRepository : RepositoryEF<ProgressRecord>, IProgressRecordRepository
    {
        public ProgressRecordRepository(AppDbContext context) : base(context)
        {
        }
    }
}
