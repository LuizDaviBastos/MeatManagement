using MeatManager.Model.Entities;
using OrderEntity = MeatManager.Data.Entities.Order;

namespace MeatManager.Data.Mapping
{
    public static class OrderMapper
    {
        public static Order ToModel(this OrderEntity entity)
        {
            if (entity == null) return null;

            return new Order
            {
                Id = entity.Id,
                OrderDate = entity.OrderDate,
                Buyer = entity.Buyer?.ToModel() ?? new(),
                Items = entity.Items?.Select(i => i.ToModel()).ToList() ?? new()
            };
        }

        public static OrderEntity ToEntity(this Order model)
        {
            if (model == null) return null;

            return new OrderEntity
            {
                Id = model.Id,
                OrderDate = model.OrderDate,
                BuyerId = model.Buyer?.Id ?? Guid.Empty
            };
        }
    }
}
