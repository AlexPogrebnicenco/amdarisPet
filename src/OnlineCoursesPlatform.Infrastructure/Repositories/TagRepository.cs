using Microsoft.EntityFrameworkCore;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Domain.Entities;
using OnlineCoursesPlatform.Infrastructure.Persistence;

namespace OnlineCoursesPlatform.Infrastructure.Repositories
{
    public class TagRepository : RepositoryEF<Tag>, ITagRepository
    {
        public TagRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<List<Tag>> GetAllOrderedAsync()
        {
            return await _context.Tags
                .Include(t => t.Category)
                .OrderBy(t => t.Name)
                .ToListAsync();
        }
    }
}
