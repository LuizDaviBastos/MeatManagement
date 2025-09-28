namespace MeatManager.Service.Interfaces
{
    public interface ICurrencyConversionService
    {
        Task<decimal> ConvertToBRLAsync(decimal amount, string currencyCode);
    }
}
