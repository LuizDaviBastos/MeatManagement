namespace MeatManager.Service.DTOs
{
    public class OrderDto
    {
        public Guid? Id { get; set; }
        public BuyerDto Buyer { get; set; }
        public DateTime? CreatedAt { get; set; }
        public ICollection<OrderItemDto> Items { get; set; }
    }
}
