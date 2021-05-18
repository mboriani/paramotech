using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Sat.Recruitment.Api.Data
{
    public abstract class WritableEntityRepository<TEntity, TId>
        : ReadableEntityRepository<TEntity, TId>, IWritableEntityRepository<TEntity, TId> where TEntity : class
    {
        protected readonly DbContext dbContext;

        protected WritableEntityRepository(DbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public virtual async Task<TEntity> Add(TEntity entity)
        {
            await dbSet.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task Delete(TEntity entity)
        {
            dbSet.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public virtual async Task Update(TId id, TEntity entity)
        {
            dbContext.Update(entity);
            await dbContext.SaveChangesAsync();
        }
    }

    public interface IWritableEntityRepository<TEntity, TId> : IReadableEntityRepository<TEntity, TId> where TEntity : class
    {
        Task<TEntity> Add(TEntity entity);
        Task Delete(TEntity entity);
        Task Update(TId id, TEntity entity);
    }
}