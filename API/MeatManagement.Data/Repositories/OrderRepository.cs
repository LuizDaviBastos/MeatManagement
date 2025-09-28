using Mapster;
using MeatManager.Data.Data;
using MeatManager.Model.Entities;
using MeatManager.Model.Interfaces;
using MeatManager.Service.Repositories;
using Microsoft.EntityFrameworkCore;
using OrderEntity = MeatManager.Data.Entities.Order;

namespace MeatManager.Data.Repositories
{
    public class OrderRepository : Repository<OrderEntity, Guid>, IOrderRepository
    {
        public OrderRepository(MeatManagerContext context) : base(context) { }

        public async Task<bool> HasOrdersAsync(Guid buyerId)
        {
            return await context.Set<OrderEntity>().AnyAsync(o => o.BuyerId.Equals(buyerId));
        }

        public async Task<Order> SaveAsync(Order entity)
        {
            var orderEntity = entity.Adapt<OrderEntity>();
            var savedEntity = await base.SaveAsync(orderEntity);
            return savedEntity.Adapt<Order>();
        }

        public async Task<Order> UpdateAsync(Order entity)
        {
            var orderEntity = entity.Adapt<OrderEntity>();
            var updatedEntity = await base.UpdateAsync(orderEntity);
            return updatedEntity.Adapt<Order>();
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            var data = await base.GetAllAsync();
            return data.Adapt<IEnumerable<Order>>();
        }

        public async Task<Order?> GetAsync(Guid id)
        {
            var data = await base.GetAsync(id);
            return data?.Adapt<Order>();
        }
    }
}
