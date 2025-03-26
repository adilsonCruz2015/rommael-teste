using Rommanel.Core.ValueObject;
using System.Linq.Expressions;

namespace Rommanel.Core.Interfaces.Common
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetById(Guid id);
        Task<IEnumerable<T>> GetAll();
        Task AddAsync(T entity);
        Task<int> DeleteAsync(T entity);
        Task UpdateAsync(T entity);
        Task<int> QueryCountAsync(Expression<Func<T, bool>> predicate = null);
        IQueryable<T> Query();
        Task<T?> GetByIdDetached(int id);

        Task<PagedResponse<T>> Filter(int pageNumber, int pageSize, Expression<Func<T, bool>>? predicate = null);
        Task<int> SaveChangesAsync();
    }
}
