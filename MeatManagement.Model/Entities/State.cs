using MeatManager.Model.Interfaces;

namespace MeatManager.Model.Entities
{
    public class State : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string UF { get; set; }
        public string Name { get; set; }
        public ICollection<City> Cities { get; set; }
    }
}
