using Mapster;
using MeatManager.Data.Data;
using MeatManager.Model.Entities;
using MeatManager.Model.Interfaces;
using MeatManager.Service.Repositories;
using Microsoft.EntityFrameworkCore;
using MeatEntity = MeatManager.Data.Entities.Meat;

namespace MeatManager.Data.Repositories
{
    public class MeatRepository : Repository<MeatEntity, Guid>, IMeatRepository
    {
        public MeatRepository(MeatManagerContext context) : base(context) { }

        public async Task<bool> HasOrdersAsync(Guid meatId)
        {
            return await context.Set<MeatEntity>().AnyAsync(m => m.Id.Equals(meatId) && m.OrderItems.Any());
        }

        public async Task<Meat> SaveAsync(Meat entity)
        {
            var meatEntity = entity.Adapt<MeatEntity>();
            var savedEntity = await base.SaveAsync(meatEntity);
            return savedEntity.Adapt<Meat>();
        }

        public async Task<Meat> UpdateAsync(Meat entity)
        {
            var meatEntity = entity.Adapt<MeatEntity>();
            var updatedEntity = await base.UpdateAsync(meatEntity);
            return updatedEntity.Adapt<Meat>();
        }

        public async Task<IEnumerable<Meat>> GetAllAsync()
        {
            var data = await base.GetAllAsync();
            return data.Adapt<IEnumerable<Meat>>();
        }

        public async Task<Meat?> GetAsync(Guid id)
        {
            var data = await base.GetAsync(id);
            return data?.Adapt<Meat>();
        }
    }
}
