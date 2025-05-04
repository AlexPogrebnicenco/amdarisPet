namespace OnlineCoursesPlatform.Application.Repositories
{
    public class InMemoryRepository<T> : IRepository<T> where T : class
    {
        private readonly List<T> _storage = new List<T>();

        public void Add(T entity) => _storage.Add(entity);

        public void Delete(int id)
        {
            var entity = _storage.FirstOrDefault(e => (e as dynamic).Id == id);
            if (entity != null)
            {
                _storage.Remove(entity);
            }
            else
            {
                throw new InvalidOperationException($"Entity with id {id} not found.");
            }
        }

        public IEnumerable<T> GetAll() => _storage;

        public T GetById(int id)
        {
            var entity = _storage.FirstOrDefault(e => (e as dynamic).Id == id);
            if (entity == null)
            {
                throw new InvalidOperationException($"Entity with id {id} not found.");
            }
            return entity;
        }

        public void Update(T entity)
        {
            var index = _storage.FindIndex(e => (e as dynamic).Id == (entity as dynamic).Id);
            if (index != -1)
            {
                _storage[index] = entity;
            }
            else
            {
                throw new InvalidOperationException($"Entity with id {(entity as dynamic).Id} not found for update.");
            }
        }
    }
}
