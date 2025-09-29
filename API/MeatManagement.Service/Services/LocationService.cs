using MeatManager.Model.Entities;
using MeatManager.Model.Interfaces;
using MeatManager.Service.Interfaces;
using MeatManager.Service.Resources;

namespace MeatManager.Service.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository locationRepository;

        public LocationService(ILocationRepository locationRepository)
        {
            this.locationRepository = locationRepository;
        }

        public async Task<ServiceResult<IEnumerable<State>>> GetStatesAsync()
        {
            var states = await locationRepository.GetStatesAsync();
            if (states == null || !states.Any())
                return ServiceResult<IEnumerable<State>>.Fail(Messages.ItemNotFound);

            return ServiceResult<IEnumerable<State>>.Ok(states);
        }

        public async Task<ServiceResult<IEnumerable<City>>> GetCitiesByStateAsync(Guid stateId)
        {
            var cities = await locationRepository.GetCitiesByStateAsync(stateId);
            if (cities == null || !cities.Any())
                return ServiceResult<IEnumerable<City>>.Fail(Messages.ItemNotFound);

            return ServiceResult<IEnumerable<City>>.Ok(cities);
        }
    }
}
