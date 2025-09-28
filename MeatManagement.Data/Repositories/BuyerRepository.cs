using Mapster;
using MeatManager.Data.Data;
using MeatManager.Model.Entities;
using MeatManager.Model.Interfaces;
using MeatManager.Service.Repositories;
using Microsoft.EntityFrameworkCore;
using BuyerEntity = MeatManager.Data.Entities.Buyer;

namespace MeatManager.Data.Repositories
{
    public class BuyerRepository : Repository<BuyerEntity, Guid>, IBuyerRepository
    {
        public BuyerRepository(MeatManagerContext context) : base(context) { }

        public async Task<bool> HasOrdersAsync(Guid id)
        {
            return await context.Set<BuyerEntity>().AnyAsync(b => b.Id.Equals(id) && b.Orders.Any());
        }

        public async Task<Buyer> SaveAsync(Buyer entity)
        {
            var buyerEntity = entity.Adapt<BuyerEntity>();
            var savedEntity = await base.SaveAsync(buyerEntity);
            return savedEntity.Adapt<Buyer>();
        }

        public async Task<Buyer> UpdateAsync(Buyer entity)
        {
            var buyerEntity = entity.Adapt<BuyerEntity>();
            var updatedEntity = await base.UpdateAsync(buyerEntity);
            return updatedEntity.Adapt<Buyer>();
        }

        public async Task<IEnumerable<Buyer>> GetAllAsync()
        {
            var data = await base.GetAllAsync();
            return data.Adapt<IEnumerable<Buyer>>();
        }

        public async Task<Buyer?> GetAsync(Guid id)
        {
            var data = await base.GetAsync(id);
            return data?.Adapt<Buyer>();
        }
    }
}
