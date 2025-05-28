namespace OnlineCoursesPlatform.Domain.Repositories
{
    public interface IRepositoryEF<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task RemoveAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync(int pageNumber, int pageSize);
        Task<T> UpdateAsync(T entity);
    }
}
