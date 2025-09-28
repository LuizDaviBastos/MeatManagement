using MeatManager.Model.Interfaces;

namespace MeatManager.Data.Entities
{
    public class OrderItem : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public double Quantity { get; set; }
        public decimal PricePerKg { get; set; }
        public decimal Total { get; set; }

        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }
        public Guid MeatId { get; set; }
        public virtual Meat Meat { get; set; }
    }
}
