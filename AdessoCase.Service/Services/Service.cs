using Microsoft.EntityFrameworkCore;
using AdessoCase.Core.Repositories;
using AdessoCase.Core.Services;
using AdessoCase.Core.UnitOfWorks;
using AdessoCase.Service.Exceptions;
using System.Linq.Expressions;

namespace AdessoCase.Service.Services
{
    public class Service<T> : IService<T> where T : class
    {
        private readonly IGenericRepository<T> _repository;
        private readonly IUnitOfWork _unitOfWork;
        public Service(IGenericRepository<T> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }



        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync(cancellationToken);
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
        {
            await _repository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync(cancellationToken);
            return entities;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await _repository.AnyAsync(expression, cancellationToken);
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken token = default)
        {
            return await _repository.GetAll().ToListAsync(token);
        }

        public async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var hasProduct = await _repository.GetByIdAsync(id, cancellationToken);

            if (hasProduct == null)
            {
                throw new NotFoundExcepiton($"{typeof(T).Name}({id}) not found");
            }
            return hasProduct;
        }

        public async Task RemoveAsync(T entity, CancellationToken cancellationToken)
        {
            _repository.Remove(entity);
            await _unitOfWork.CommitAsync(cancellationToken);
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
        {
            _repository.RemoveRange(entities);
            await _unitOfWork.CommitAsync(cancellationToken);
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            _repository.Update(entity);
            await _unitOfWork.CommitAsync(cancellationToken);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _repository.Where(expression);
        }
    }
}
