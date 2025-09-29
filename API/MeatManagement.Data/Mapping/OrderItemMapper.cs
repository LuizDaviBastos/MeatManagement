using MeatManager.Model.Entities;
using OrderItemEntity = MeatManager.Data.Entities.OrderItem;

namespace MeatManager.Data.Mapping
{
    public static class OrderItemMapper
    {
        public static OrderItem ToModel(this OrderItemEntity entity)
        {
            if (entity == null) return null;

            return new OrderItem
            {
                Id = entity.Id,
                CurrencyCode = entity.CurrencyCode,
                Price = entity.Price,
                Meat = entity.Meat?.ToModel() ?? new()
            };
        }

        public static OrderItemEntity ToEntity(this OrderItem model, Guid orderId)
        {
            if (model == null) return null;

            return new OrderItemEntity
            {
                Id = model.Id,
                Price = model.Price,
                CurrencyCode = model.CurrencyCode,
                MeatId = model.Meat?.Id ?? Guid.Empty,
                OrderId = orderId
            };
        }
    }
}
