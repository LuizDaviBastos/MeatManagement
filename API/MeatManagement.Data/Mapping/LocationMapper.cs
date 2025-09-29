using MeatManager.Model.Entities;
using CityEntity = MeatManager.Data.Entities.City;
using StateEntity = MeatManager.Data.Entities.State;
namespace MeatManager.Data.Mapping
{
    public static class LocationMapper
    {
         public static City ToModel(this CityEntity entity)
        {
            if (entity == null) return null;

            return new City
            {
                Id = entity.Id,
                Name = entity.Name,
                State = entity.State?.ToModel() ?? new()
            };
        }

        public static State ToModel(this StateEntity entity)
        {
            if (entity == null) return null;

            return new State
            {
                Id = entity.Id,
                Name = entity.Name,
                UF = entity.UF
            };
        }
    }
}