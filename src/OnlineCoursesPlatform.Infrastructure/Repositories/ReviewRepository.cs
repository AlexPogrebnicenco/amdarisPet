using Microsoft.EntityFrameworkCore;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Domain.Entities;
using OnlineCoursesPlatform.Infrastructure.Persistence;

namespace OnlineCoursesPlatform.Infrastructure.Repositories
{
    public class ReviewRepository : RepositoryEF<Review>, IReviewRepository
    {
        public ReviewRepository(AppDbContext context) : base(context)
        {
        }
    }
}
