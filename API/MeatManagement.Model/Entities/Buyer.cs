using MeatManager.Model.Interfaces;
using MeatManager.Model.ValueObjects;

namespace MeatManager.Model.Entities
{
    public class Buyer : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Document Document { get; set; }
        public DateTime CreatedAt { get; set; }
        public Address? Address { get; set; }

        public void SetDocument(string value)
        {
            Document = new Document(value);
        }
    }
}
