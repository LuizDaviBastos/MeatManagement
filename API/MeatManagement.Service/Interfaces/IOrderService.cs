using MeatManager.Service.DTOs;

namespace MeatManager.Service.Interfaces
{
    public interface IOrderService
    {
        Task<ServiceResult<IEnumerable<OrderDto>>> GetAllAsync();
        Task<ServiceResult<OrderDto>> GetByIdAsync(Guid id);
        Task<ServiceResult<OrderDto>> CreateAsync(OrderDto order);
        Task<ServiceResult<OrderDto>> UpdateAsync(Guid id, OrderDto order);
        Task<ServiceResult<bool>> DeleteAsync(Guid id);
    }
}
