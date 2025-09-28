using MeatManager.Model.Entities;

namespace MeatManager.Service.Interfaces
{
    public interface ILocationService
    {
        Task<IEnumerable<State>> GetStatesAsync();
        Task<IEnumerable<City>> GetCitiesByStateAsync(Guid stateId);
    }
}
