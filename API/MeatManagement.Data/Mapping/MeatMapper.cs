using MeatManager.Model.Entities;
using MeatManager.Data.Entities.Enums;
using MeatEntity = MeatManager.Data.Entities.Meat;

namespace MeatManager.Data.Mapping
{
    public static class MeatMapper
    {
        public static Meat ToModel(this MeatEntity entity)
        {
            if (entity == null) return null;

            return new Meat
            {
                Id = entity.Id,
                Name = entity.Name,
                CreatedAt = entity.CreatedAt,
                Origin = (int)entity.Origin
            };
        }

        public static MeatEntity ToEntity(this Meat model)
        {
            if (model == null) return null;

            return new MeatEntity
            {
                Id = model.Id,
                Name = model.Name,
                CreatedAt = model.CreatedAt,
                Origin = (MeatOrigin)model.Origin
            };
        }
    }
}
