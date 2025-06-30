using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Application.Interfaces.Repositories;
using OnlineCoursesPlatform.Infrastructure.Persistence;

namespace OnlineCoursesPlatform.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context,
            IUserRepository userRepository,
            ICategoryRepository categoryRepository,
            ICertificateRepository certificateRepository,
            ICourseRepository courseRepository,
            ICourseTagRepository courseTagRepository,
            IEnrollmentRepository enrollmentRepository,
            ILessonRepository lesson,
            IProgressRecordRepository progressRecordRepository,
            IReviewRepository reviewRepository,
            ITagRepository tagRepository,
            IRefreshTokenRepository refreshTokenRepository,
            ITeacherRegistrationRequestRepository teacherRegistrationRequestRepository,
            ISetPasswordTokenRepository setPasswordTokenRepository,
            ICourseAuthorRepository courseAuthorRepository
            )
        {
            _context = context;
            UserRepository = userRepository;
            CategoryRepository = categoryRepository;
            CertificateRepository = certificateRepository;
            CourseRepository = courseRepository;
            CourseTagRepository = courseTagRepository;
            EnrollmentRepository = enrollmentRepository;
            LessonRepository = lesson;
            ProgressRecordRepository = progressRecordRepository;
            ReviewRepository = reviewRepository;
            TagRepository = tagRepository;
            RefreshTokenRepository = refreshTokenRepository;
            TeacherRegistrationRequestRepository = teacherRegistrationRequestRepository;
            SetPasswordTokenRepository = setPasswordTokenRepository;
            CourseAuthorRepository = courseAuthorRepository;
        }

        public IUserRepository UserRepository { get; private set; }
        public ICategoryRepository CategoryRepository { get; private set; }
        public ICertificateRepository CertificateRepository { get; private set; }
        public ICourseRepository CourseRepository { get; private set; }
        public ICourseTagRepository CourseTagRepository { get; private set; }
        public IEnrollmentRepository EnrollmentRepository { get; private set; }
        public ILessonRepository LessonRepository {  get; private set; }
        public IProgressRecordRepository ProgressRecordRepository { get; private set; }
        public IReviewRepository ReviewRepository { get; private set; }
        public ITagRepository TagRepository { get; private set; }
        public IRefreshTokenRepository RefreshTokenRepository { get; }

        public ITeacherRegistrationRequestRepository TeacherRegistrationRequestRepository { get; private set; }

        public ISetPasswordTokenRepository SetPasswordTokenRepository { get; private set; }

        public ICourseAuthorRepository CourseAuthorRepository { get; }



        public async Task BeginTransactionAsync()
        {
            await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _context.Database.CommitTransactionAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
