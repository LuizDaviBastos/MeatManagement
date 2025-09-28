using MeatManager.Service.DTOs;

namespace MeatManager.Service.Interfaces
{
    public interface IMeatService
    {
        Task<ServiceResult<IEnumerable<MeatDto>>> GetAllAsync();
        Task<ServiceResult<MeatDto>> GetByIdAsync(Guid id);
        Task<ServiceResult<MeatDto>> CreateAsync(MeatDto dto);
        Task<ServiceResult<MeatDto>> UpdateAsync(Guid id, MeatDto dto);
        Task<ServiceResult<bool>> DeleteAsync(Guid id);
    }
}
