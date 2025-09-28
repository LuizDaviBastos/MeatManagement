using MeatManager.Model.Interfaces;

namespace MeatManager.Data.Entities
{
    public class Address : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public Guid BuyerId { get; set; }
        public Guid StateId { get; set; }
        public Guid CityId { get; set; }
        public virtual Buyer Buyer { get; set; }
        public virtual State State { get; set; }
        public virtual City City { get; set; }
    }
}
