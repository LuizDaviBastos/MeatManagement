namespace MeatManager.Service.DTOs
{
    public class MeatDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public decimal PricePerKg { get; set; }
        public double WeightKg { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
