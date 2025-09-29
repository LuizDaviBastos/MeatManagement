using MeatManager.Model.Interfaces;

namespace MeatManager.Model.Entities
{
    public class Meat : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Origin { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
