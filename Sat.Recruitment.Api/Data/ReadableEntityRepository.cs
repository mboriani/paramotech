using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Sat.Recruitment.Api.Data
{
    public abstract class ReadableEntityRepository<TEntity, TId> : IReadableEntityRepository<TEntity, TId> where TEntity : class
    {
        protected readonly DbSet<TEntity> dbSet;

        protected ReadableEntityRepository(DbContext dbContext)
        {
            dbSet = dbContext.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, object>> include)
        {
            return await this.dbSet.Include(include).ToListAsync<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> Get()
        {
            return await this.dbSet.ToListAsync<TEntity>();
        }

        public virtual async Task<TEntity> FindByIdAsync(TId id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return await dbSet.Where(predicate).ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> FindByInclude(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> include)
        {
            return await dbSet.Include(include).Where(predicate).ToListAsync();
        }
    }

    public interface IReadableEntityRepository<TEntity, TId>
    {
        Task<IEnumerable<TEntity>> Get();
        Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, object>> include);
        Task<TEntity> FindByIdAsync(TId id);
        Task<IEnumerable<TEntity>> FindBy(Expression<Func<TEntity, bool>> predicate);

        Task<IEnumerable<TEntity>> FindByInclude(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, object>> include);
    }
}