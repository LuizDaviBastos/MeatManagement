using MeatManager.Service.DTOs;

namespace MeatManager.Service.Interfaces
{
    public interface IBuyerService
    {
        Task<ServiceResult<IEnumerable<BuyerDto>>> GetAllAsync();
        Task<ServiceResult<BuyerDto>> GetByIdAsync(Guid id);
        Task<ServiceResult<BuyerDto>> CreateAsync(BuyerDto buyer);
        Task<ServiceResult<BuyerDto>> UpdateAsync(Guid id, BuyerDto buyer);
        Task<ServiceResult<bool>> DeleteAsync(Guid id);
    }
}
