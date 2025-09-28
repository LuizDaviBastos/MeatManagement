using MeatManager.Model.Interfaces;

namespace MeatManager.Data.Entities
{
    public class City : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid StateId { get; set; }
        public virtual State State { get; set; }
    }
}
