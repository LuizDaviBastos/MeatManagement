using FluentValidation;
using Mapster;
using MeatManager.Model.Entities;
using MeatManager.Model.Interfaces;
using MeatManager.Service.DTOs;
using MeatManager.Service.Interfaces;
using MeatManager.Service.Resources;

namespace MeatManager.Service.Services
{

    namespace MeatManager.Service.Services
    {
        public class BuyerService : IBuyerService
        {
            private readonly IBuyerRepository buyerRepository;
            private readonly IValidator<BuyerDto> validator;

            public BuyerService(IBuyerRepository buyerRepository, IValidator<BuyerDto> validator)
            {
                this.buyerRepository = buyerRepository;
                this.validator = validator;
            }

            public async Task<ServiceResult<BuyerDto>> CreateAsync(BuyerDto dto)
            {
                var validationResult = await validator.ValidateAsync(dto);
                if (!validationResult.IsValid)
                    return ServiceResult<BuyerDto>.Fail(validationResult.Errors.Select(e => e.ErrorMessage));

                var buyer = dto.Adapt<Buyer>();
                buyer.CreatedAt = DateTime.UtcNow;

                var response = await buyerRepository.SaveAsync(buyer);
                return response.Adapt<BuyerDto>();
            }

            public async Task<ServiceResult<bool>> DeleteAsync(Guid id)
            {
                var hasOrders = await buyerRepository.HasOrdersAsync(id);
                if (hasOrders)
                    return ServiceResult<bool>.Fail(Messages.BuyerHasOrders);

                var deleted = await buyerRepository.DeleteAsync(id);
                if (deleted)
                    return ServiceResult<bool>.Ok(true);

                return ServiceResult<bool>.Fail(Messages.DeleteItemFailed);
            }

            public async Task<ServiceResult<IEnumerable<BuyerDto>>> GetAllAsync()
            {
                var buyers = await buyerRepository.GetAllAsync();
                var buyerDtos = buyers.Adapt<IEnumerable<BuyerDto>>();
                return ServiceResult<IEnumerable<BuyerDto>>.Ok(buyerDtos);
            }

            public async Task<ServiceResult<BuyerDto>> GetByIdAsync(Guid id)
            {
                var buyer = await buyerRepository.GetAsync(id);
                if (buyer == null)
                    return ServiceResult<BuyerDto>.Fail(Messages.ItemNotFound);

                return buyer.Adapt<BuyerDto>();
            }

            public async Task<ServiceResult<BuyerDto>> UpdateAsync(Guid id, BuyerDto dto)
            {
                var validationResult = await validator.ValidateAsync(dto);
                if (!validationResult.IsValid)
                    return ServiceResult<BuyerDto>.Fail(validationResult.Errors.Select(e => e.ErrorMessage));

                var buyer = dto.Adapt<Buyer>();
                buyer.Id = id;

                var response = await buyerRepository.UpdateAsync(buyer);
                return response.Adapt<BuyerDto>();
            }
        }
    }

}
