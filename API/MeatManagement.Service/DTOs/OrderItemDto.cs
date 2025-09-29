namespace MeatManager.Service.DTOs
{
    public class OrderItemDto
    {
        public Guid? Id { get; set; }
        public string? MeatId { get; set; }
        public string? CurrencyCode { get; set; }
        public decimal? Price { get; set; }
        public decimal PriceBRL { get; internal set; }
    }
}
