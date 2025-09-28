using MeatManager.Model.Interfaces;

namespace MeatManager.Model.Entities
{
    public class Buyer : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public string DocumentType { get; set; }
        public DateTime CreatedAt { get; set; }
        public Address Address { get; set; }
    }
}
