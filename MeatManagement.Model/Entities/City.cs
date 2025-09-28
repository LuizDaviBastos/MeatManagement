using MeatManager.Model.Interfaces;

namespace MeatManager.Model.Entities
{
    public class City : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UF { get; set; }
        public State State { get; set; }
    }
}
