using MeatManager.Data.Entities.Enums;
using MeatManager.Model.Interfaces;

namespace MeatManager.Data.Entities
{
    public class Meat : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public MeatOrigin Origin { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }

}
