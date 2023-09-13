using System.Linq.Expressions;

namespace AdessoCase.Core.Services
{
    public interface IService<T> where T : class
    {
        Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetAllAsync(CancellationToken token);
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
        Task<T> AddAsync(T entity,CancellationToken cancellationToken);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task RemoveAsync(T entity, CancellationToken cancellationToken = default);
        Task RemoveRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);


    }
}
