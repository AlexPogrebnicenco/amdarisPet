using OnlineCoursesPlatform.Application.Interfaces.Repositories;
using OnlineCoursesPlatform.Domain.Entities;
using OnlineCoursesPlatform.Domain.Repositories;

namespace OnlineCoursesPlatform.Application.Abstractions.Repositories
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        ICertificateRepository CertificateRepository { get; }
        ICourseRepository CourseRepository { get; }
        ICourseTagRepository CourseTagRepository { get; }
        ICourseTeacherRepository CourseTeacherRepository { get; }
        IEnrollmentRepository EnrollmentRepository { get;}
        ILessonRepository LessonRepository { get; }
        IProgressRecordRepository ProgressRecordRepository { get; }
        IReviewRepository ReviewRepository { get; }
        ITagRepository TagRepository { get; }
        ITeacherRepository TeacherRepository { get; }
        IRefreshTokenRepository RefreshTokenRepository { get; }


        Task SaveAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
