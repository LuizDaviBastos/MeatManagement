using MeatManager.Model.Interfaces;

namespace MeatManager.Model.Entities
{
    public class OrderItem : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public double QuantityKg { get; set; }
        public decimal PricePerKg { get; set; }
        public decimal Total => PricePerKg * (decimal)QuantityKg;
        public Meat Meat { get; set; }
    }
}
