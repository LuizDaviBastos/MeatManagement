using MeatManager.Model.Interfaces;

namespace MeatManager.Data.Entities
{
    public class Order : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid BuyerId { get; set; }
        public virtual Buyer Buyer { get; set; }
        public virtual ICollection<OrderItem> Items { get; set; }

    }
}
