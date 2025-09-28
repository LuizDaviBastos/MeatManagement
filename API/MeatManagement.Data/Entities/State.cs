using MeatManager.Model.Interfaces;

namespace MeatManager.Data.Entities
{
    public class State : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }  
        public string UF { get; set; } 
        public virtual ICollection<City> Cities { get; set; }
    }
}
