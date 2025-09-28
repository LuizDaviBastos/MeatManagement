using MeatManager.Data.Data;
using MeatManager.Data.Interfaces;
using MeatManager.Model.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace MeatManager.Service.Repositories
{
    public class Repository<TEntity, Guid> : IRepository<TEntity, Guid> where TEntity : class, IEntity<Guid>
    {
        protected readonly MeatManagerContext context;
        public Repository(MeatManagerContext context)
        {
            this.context = context;
        }

        public async virtual Task<bool> DeleteAsync(Guid id)
        {
            var affected = await context.Set<TEntity>().Where(e => e.Id.Equals(id)).ExecuteDeleteAsync();
            return affected > 0;
        }

        public virtual async Task<TEntity?> GetAsync(Guid id)
        {
            return await context.Set<TEntity>().Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expression)
        {
            return context.Set<TEntity>().Where(expression);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await context.Set<TEntity>().ToListAsync();
        }

        public async virtual Task<TEntity> SaveAsync(TEntity entity)
        {
            await context.Set<TEntity>().AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async virtual Task<TEntity> UpdateAsync(TEntity entity)
        {
            context.Set<TEntity>().Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task ExecuteUpdateAsync(Expression<Func<TEntity, bool>> predicate, 
            Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setPropertyCalls)
        {
            await context.Set<TEntity>().Where(predicate).ExecuteUpdateAsync(setPropertyCalls);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await context.Set<TEntity>().AnyAsync(X => X.Id.Equals(id));
        }
    }
}
