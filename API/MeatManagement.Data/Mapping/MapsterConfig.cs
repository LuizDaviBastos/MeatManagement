using Mapster;
using MeatManager.Model.Entities;
using MeatManager.Model.Enums;
using BuyerEntity = MeatManager.Data.Entities.Buyer;

namespace MeatManager.Data.Mapping
{
    public static class MapsterConfig
    {
        public static void RegisterMappings()
        {
            TypeAdapterConfig<Buyer, BuyerEntity>
                .NewConfig()
                .Map(dest => dest.Document, src => src.Document.Value)
                .Map(dest => dest.DocumentType, src => src.Document.Type == DocumentType.CPF ? Entities.Enums.DocumentType.CPF : Entities.Enums.DocumentType.CNPJ)
                .Map(dest => dest.Address, src => src.Address);

            TypeAdapterConfig<BuyerEntity, Buyer>
                .NewConfig()
                .Ignore(dest => dest.Document)
                .ConstructUsing(entity => new()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    CreatedAt = entity.CreatedAt,
                    Address = entity.Address != null
                    ? new Model.ValueObjects.Address
                    {
                        CityId = entity.Address.CityId,
                        StateId = entity.Address.StateId
                    } : null,
                    Document = new Model.ValueObjects.Document(entity.Document)
                });
        }
    }
}