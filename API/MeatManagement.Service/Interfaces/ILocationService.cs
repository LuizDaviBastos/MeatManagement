using MeatManager.Model.Entities;

namespace MeatManager.Service.Interfaces
{
    public interface ILocationService
    {
        Task<ServiceResult<IEnumerable<State>>> GetStatesAsync();
        Task<ServiceResult<IEnumerable<City>>> GetCitiesByStateAsync(Guid stateId);
    }
}
