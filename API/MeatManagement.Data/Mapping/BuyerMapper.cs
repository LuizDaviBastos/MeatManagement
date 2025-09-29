using MeatManager.Model.Entities;
using BuyerEntity = MeatManager.Data.Entities.Buyer;
using AddressEntity = MeatManager.Data.Entities.Address;
using MeatManager.Model.ValueObjects;

namespace MeatManager.Data.Mapping
{
    public static class BuyerMapper
    {
        public static Buyer ToModel(this BuyerEntity entity)
        {
            if (entity == null) return null;

            var model = new Buyer
            {
                Id = entity.Id,
                Name = entity.Name,
                CreatedAt = entity.CreatedAt,
                Address = entity.Address != null
                    ? new Model.ValueObjects.Address
                    {
                        StateId = entity.Address.StateId,
                        CityId = entity.Address.CityId
                    }
                    : null
            };
            model.SetDocument(entity.Document);

            return model;
        }

        public static BuyerEntity ToEntity(this Buyer model)
        {
            if (model == null) return null;

            return new BuyerEntity
            {
                Id = model.Id,
                Name = model.Name,
                CreatedAt = model.CreatedAt,
                Address = model.Address.ToEntity(model.Id),
                Document = model.Document.Value,
                DocumentType = model.Document.Type == Model.Enums.DocumentType.CPF ? Entities.Enums.DocumentType.CPF : Entities.Enums.DocumentType.CNPJ,
            };
        }

        public static AddressEntity ToEntity(this Address? model, Guid buyerId)
        {
            if (model == null || !model.StateId.HasValue 
                || model.StateId == Guid.Empty) return null;

            return new AddressEntity
            {
                BuyerId = buyerId,
                CityId = model.CityId,
                StateId = model.StateId
            };
        }
    }
}
