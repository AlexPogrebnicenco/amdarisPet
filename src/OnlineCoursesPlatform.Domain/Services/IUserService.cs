using OnlineCoursesPlatform.Domain.Entities;

namespace OnlineCoursesPlatform.Domain.Services
{
    public interface IUserService
    {
        void Add(User entity);
        void Delete(int id);
        IEnumerable<User> GetAll();
        User GetById(int id);
        void Update(User entity);
        int GetLastId();
    }
}
