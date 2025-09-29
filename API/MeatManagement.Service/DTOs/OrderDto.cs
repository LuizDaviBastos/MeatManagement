namespace MeatManager.Service.DTOs
{
    public class OrderDto
    {
        public Guid? Id { get; set; }
        public BuyerDto Buyer { get; set; }
        public decimal Total { get; set; }
        public decimal TotalBRL { get; set; }
        public DateTime? OrderDate { get; set; }
        public ICollection<OrderItemDto> Items { get; set; }
    }
}
