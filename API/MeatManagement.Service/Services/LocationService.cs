using MeatManager.Model.Entities;
using MeatManager.Model.Interfaces;
using MeatManager.Service.Interfaces;

namespace MeatManager.Service.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository locationRepository;
        public LocationService(ILocationRepository locationRepository)
        {
            this.locationRepository = locationRepository;
        }

        public async Task<IEnumerable<State>> GetStatesAsync()
        {
            return await locationRepository.GetStatesAsync();
        }

        public async Task<IEnumerable<City>> GetCitiesByStateAsync(Guid stateId)
        {
            return await locationRepository.GetCitiesByStateAsync(stateId);
        }
    }
}