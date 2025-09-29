using MeatManager.Model.Interfaces;

namespace MeatManager.Model.Entities
{
    public class OrderItem : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string CurrencyCode { get; set; }
        public decimal Price { get; set; }
        public Meat Meat { get; set; }
    }
}
