using MeatManager.Data.Data;
using MeatManager.Data.Mapping;
using MeatManager.Model.Entities;
using MeatManager.Model.Interfaces;
using MeatManager.Service.Repositories;
using Microsoft.EntityFrameworkCore;
using OrderEntity = MeatManager.Data.Entities.Order;
using OrderItemEntity = MeatManager.Data.Entities.OrderItem;

namespace MeatManager.Data.Repositories
{
    public class OrderRepository : Repository<OrderEntity, Guid>, IOrderRepository
    {
        public OrderRepository(MeatManagerContext context) : base(context) { }

        public async Task<bool> HasOrdersAsync(Guid buyerId)
        {
            return await context.Set<OrderEntity>().AnyAsync(o => o.BuyerId.Equals(buyerId));
        }

        public async Task<Order> SaveAsync(Order model)
        {
            var orderEntity = model.ToEntity();
            
            var savedEntity = await base.SaveAsync(orderEntity);
            var items = model.Items.Select(i => i.ToEntity(savedEntity.Id));
            context.Set<OrderItemEntity>().AddRange(items);
            await context.SaveChangesAsync();
            return savedEntity.ToModel();
        }

        public async Task<Order> UpdateAsync(Order model)
        {
            var orderEntity = model.ToEntity();
            var updatedItemIds = model.Items.Select(oi => oi.Id);

            await context.Set<OrderItemEntity>()
                .Where(oi => oi.OrderId == model.Id && !updatedItemIds.Contains(oi.Id))
                .ExecuteDeleteAsync();

            var items = model.Items.Select(i => i.ToEntity(model.Id));
            context.Set<OrderItemEntity>().UpdateRange(items);

            var updatedEntity = await base.UpdateAsync(orderEntity);
            return updatedEntity.ToModel();
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            var data = await base.GetAllAsync();
            return data.Select(o => o.ToModel());
        }

        public async Task<Order?> GetAsync(Guid id)
        {
            var data = await base.GetAsync(id);
            return data?.ToModel();
        }
    }
}
