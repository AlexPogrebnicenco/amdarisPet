using Microsoft.EntityFrameworkCore;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Domain.Entities;
using OnlineCoursesPlatform.Infrastructure.Persistence;

namespace OnlineCoursesPlatform.Infrastructure.Repositories
{
    public class CertificateRepository : RepositoryEF<Certificate>, ICertificateRepository
    {
        public CertificateRepository(AppDbContext context) : base(context)
        {
        }
    }
}
