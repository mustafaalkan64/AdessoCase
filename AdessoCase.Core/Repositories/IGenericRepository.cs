using System.Linq.Expressions;

namespace AdessoCase.Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        IQueryable<T> GetAll();
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
        Task AddAsync(T entity, CancellationToken cancellationToken = default);
        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);

    }
}
