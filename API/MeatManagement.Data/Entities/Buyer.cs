using MeatManager.Data.Entities.Enums;
using MeatManager.Model.Interfaces;

namespace MeatManager.Data.Entities
{
    public class Buyer : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public DocumentType DocumentType { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Order> Orders { get; set; }
        public virtual Address? Address { get; set; }
    }
}
