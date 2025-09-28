namespace MeatManager.Service.DTOs
{
    public class BuyerDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public AddressDto Address { get; set; }
    }
}
