using MeatManager.Model.Interfaces;

namespace MeatManager.Model.Entities
{
    public class Order : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public Buyer Buyer { get; set; }
        public ICollection<OrderItem> Items { get; set; }
    }
}
