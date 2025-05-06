using OnlineCoursesPlatform.Application.Interfaces;
using OnlineCoursesPlatform.Domain.Entities;

namespace OnlineCoursesPlatform.Infrastructure.Services
{
    public class ReviewService
    {
        private readonly IRepository<Review> _repository;

        public ReviewService(IRepository<Review> repository)
        {
            _repository = repository;
        }

        public void Add(Review entity)
        {
            _repository.Add(entity);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public IEnumerable<Review> GetAll()
        {
            return _repository.GetAll();
        }

        public Review GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Update(Review entity)
        {
            _repository.Update(entity);
        }
    }
}
