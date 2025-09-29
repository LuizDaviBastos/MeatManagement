using MeatManager.Data.Data;
using MeatManager.Data.Mapping;
using MeatManager.Model.Entities;
using MeatManager.Model.Interfaces;
using Microsoft.EntityFrameworkCore;
using CityEntity = MeatManager.Data.Entities.City;
using StateEntity = MeatManager.Data.Entities.State;

namespace MeatManager.Data.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly MeatManagerContext context;

        public LocationRepository(MeatManagerContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<City>> GetCitiesByStateAsync(Guid stateId)
        {
            var results = await context.Set<CityEntity>().Where(c => c.StateId == stateId).ToListAsync();
            return results.Select(c => c.ToModel());
        }

        public async Task<IEnumerable<State>> GetStatesAsync()
        {
            var results = await context.Set<StateEntity>().ToListAsync();
            return results.Select(s => s.ToModel());
        }
    }
}
