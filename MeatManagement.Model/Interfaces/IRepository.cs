using MeatManager.Model.Interfaces;

namespace MeatManager.Data.Interfaces
{
    public interface IRepository<TEntity, TKey> where TEntity : IEntity<TKey>
    {
        Task<TEntity> SaveAsync(TEntity entity);
        Task<bool> DeleteAsync(TKey id);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity?> GetAsync(TKey id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<bool> ExistsAsync(TKey id);
    }
}
