using OnlineCoursesPlatform.Application.Interfaces.Repositories;

namespace OnlineCoursesPlatform.Application.Abstractions.Repositories
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        ICertificateRepository CertificateRepository { get; }
        ICourseRepository CourseRepository { get; }
        ICourseTagRepository CourseTagRepository { get; }
        IEnrollmentRepository EnrollmentRepository { get; }
        ILessonRepository LessonRepository { get; }
        IProgressRecordRepository ProgressRecordRepository { get; }
        IReviewRepository ReviewRepository { get; }
        ITagRepository TagRepository { get; }
        IRefreshTokenRepository RefreshTokenRepository { get; }

        ITeacherRegistrationRequestRepository TeacherRegistrationRequestRepository { get; }
        ISetPasswordTokenRepository SetPasswordTokenRepository { get; }

        ICourseAuthorRepository CourseAuthorRepository { get; }


        Task SaveAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
