using Mapster;
using MeatManager.Model.Entities;
using MeatManager.Service.DTOs;
namespace MeatManager.Service.Mapping
{
    public static class MapsterConfig
    {
        public static void RegisterMappings()
        {
            TypeAdapterConfig<BuyerDto, Buyer>.NewConfig()
              .Ignore(dest => dest.Document);

            TypeAdapterConfig<Buyer, BuyerDto>.NewConfig()
             .Map(dest => dest.Document, src => src.Document.Value);
            //.Map(dest => dest.Address, src => (src.Address != null ? new )
        }
    }
}