using MeatManager.Model.Entities;

namespace MeatManager.Model.Interfaces
{
    public interface ILocationRepository
    {
        Task<IEnumerable<State>> GetStatesAsync();
        Task<IEnumerable<City>> GetCitiesByStateAsync(Guid stateId);
    }
}
