using MeatManager.Data.Data;
using MeatManager.Data.Mapping;
using MeatManager.Model.Entities;
using MeatManager.Model.Interfaces;
using MeatManager.Service.Repositories;
using Microsoft.EntityFrameworkCore;
using BuyerEntity = MeatManager.Data.Entities.Buyer;
using AddressEntity = MeatManager.Data.Entities.Address;

namespace MeatManager.Data.Repositories
{
    public class BuyerRepository : Repository<BuyerEntity, Guid>, IBuyerRepository
    {
        public BuyerRepository(MeatManagerContext context) : base(context) { }

        public async Task<bool> HasOrdersAsync(Guid id)
        {
            return await context.Set<BuyerEntity>().AnyAsync(b => b.Id.Equals(id) && b.Orders.Any());
        }

        public async Task<Buyer> SaveAsync(Buyer model)
        {
            var buyerEntity = model.ToEntity();
            
            var savedEntity = await base.SaveAsync(buyerEntity);
            return savedEntity.ToModel();
        }

        public async Task<Buyer> UpdateAsync(Buyer model)
        {
            var buyerEntity = await context.Set<BuyerEntity>()
                .Include(b => b.Address)
                .FirstOrDefaultAsync(b => b.Id == model.Id);

            buyerEntity.Name = model.Name;
            buyerEntity.Document = model.Document.Value;
            buyerEntity.Address = model.Address.ToEntity(model.Id);

            var updatedEntity = await base.UpdateAsync(buyerEntity);
            return updatedEntity.ToModel();
        }

        public async Task<IEnumerable<Buyer>> GetAllAsync()
        {
            var data = await base.GetAllAsync();
            return data.Select(b => b.ToModel());
        }

        public async Task<Buyer?> GetAsync(Guid id)
        {
            var data = await base.GetAsync(id);
            return data?.ToModel();
        }
    }
}
