

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Rommanel.Core.Helpers;
using Rommanel.Core.Interfaces.Common;
using Rommanel.Core.ValueObject;
using System.Linq.Dynamic.Core;
using System;
using Rommanel.Core.Entities.Common;

namespace Rommanel.Infra.Repositories.Common
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : Entity
    {
        protected readonly DbContextClass _dbContext;

        protected GenericRepository(DbContextClass context)
        {
            _dbContext = context;
        }

        public async Task<T?> GetById(Guid id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T?> GetByIdDetached(int id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);

            if (entity is not null)
                _dbContext.Entry(entity).State = EntityState.Detached;

            return entity;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return await SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            await SaveChangesAsync();
        }

        public async Task<PagedResponse<T>> Filter(int pageNumber,
                                                   int pageSize,
                                                   Expression<Func<T, bool>>? predicate = null)
        {
            int totalRecords = 0;

            var query = Query();

            totalRecords = await QueryCountAsync(predicate);

            // Aplicar o predicado (filtro) se existir
            if (predicate != null)            
                query = query.Where(predicate);               

            // Aplicar a ordenação e paginação
            var entities = await query
                .OrderBy(x => x.Id)
                .Skip((pageNumber -1) * pageSize) // Ajuste na paginação
                .Take(pageSize)
                .ToListAsync();

            // Retornar a resposta paginada
            var pagedResponse = new PagedResponse<T>(entities, pageNumber, pageSize, totalRecords);

            return pagedResponse;
        }

        public async Task<int> QueryPredicateAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).AsNoTracking().CountAsync();
        }

        public async Task<int> QueryCountAsync(Expression<Func<T, bool>> predicate = null)
        {
            var query = _dbContext.Set<T>().AsNoTracking();

            if (predicate != null)
                query = query.Where(predicate); // 🔹 Aplica o filtro antes de contar

            return await query.CountAsync();
        }

        public IQueryable<T> Query()
        {
            return _dbContext.Set<T>().AsNoTracking().AsQueryable();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
