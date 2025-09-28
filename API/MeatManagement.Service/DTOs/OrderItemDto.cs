namespace MeatManager.Service.DTOs
{
    public class OrderItemDto
    {
        public Guid? Id { get; set; }
        public string CurrencyCode { get; set; }
        public double QuantityKg { get; set; }
        public decimal PricePerKg { get; set; }
        public decimal Total { get; set; }
        public decimal TotalBRL { get; set; }
    }
}
