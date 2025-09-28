namespace MeatManager.Service.Models
{
    public class CurrencyRate
    {
        public string Code { get; set; }
        public decimal Rate { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
