using MeatManager.Model.Interfaces;

namespace MeatManager.Model.Entities
{
    public class Meat : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal PricePerKg { get; set; }
        public double WeightKg { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
