using OnlineCoursesPlatform.Application.Interfaces;
using OnlineCoursesPlatform.Domain.Entities;

namespace OnlineCoursesPlatform.Infrastructure.Services
{
    public class CourseService
    {
        private readonly IRepository<Course> _repository;

        public CourseService(IRepository<Course> repository)
        {
            _repository = repository;
        }

        public void Add(Course entity) 
        {
            _repository.Add(entity);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public IEnumerable<Course> GetAll() 
        {
            return _repository.GetAll();
        }

        public Course GetById(int id) 
        {
            return _repository.GetById(id);
        }

        public void Update(Course entity)
        {
            _repository.Update(entity);
        }
    }
}
