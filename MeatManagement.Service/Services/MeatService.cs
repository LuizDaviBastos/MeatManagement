using FluentValidation;
using Mapster;
using MeatManager.Model.Entities;
using MeatManager.Model.Interfaces;
using MeatManager.Service.DTOs;
using MeatManager.Service.Interfaces;
using MeatManager.Service.Resources;

namespace MeatManager.Service.Services
{
    public class MeatService : IMeatService
    {
        private readonly IMeatRepository meatRepository;
        private readonly IValidator<MeatDto> validator;
        public MeatService(IMeatRepository meatRepository, IValidator<MeatDto> validator)
        {
            this.meatRepository = meatRepository;
            this.validator = validator;
        }

        public async Task<ServiceResult<MeatDto>> CreateAsync(MeatDto dto)
        {
            var validationResult = await validator.ValidateAsync(dto);
            if (!validationResult.IsValid) 
            {
                ServiceResult<MeatDto>.Fail(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var meat = dto.Adapt<Meat>();
            meat.CreatedAt = DateTime.UtcNow;

            var response = await meatRepository.SaveAsync(meat);
            return response.Adapt<MeatDto>();
        }

        public async Task<ServiceResult<bool>> DeleteAsync(Guid id)
        {
            var hasOrders = await meatRepository.HasOrdersAsync(id);
            if (hasOrders)
                return ServiceResult<bool>.Fail(Messages.MeatHasOrders);

            var deleted = await meatRepository.DeleteAsync(id);
            if (deleted)
                return ServiceResult<bool>.Ok(true);

            return ServiceResult<bool>.Fail(Messages.DeleteItemFailed);
        }

        public async Task<ServiceResult<IEnumerable<MeatDto>>> GetAllAsync()
        {
            var meats = await meatRepository.GetAllAsync();
            var meatDtos = meats.Adapt<IEnumerable<MeatDto>>();
            return ServiceResult<IEnumerable<MeatDto>>.Ok(meatDtos);
        }

        public async Task<ServiceResult<MeatDto>> GetByIdAsync(Guid id)
        {
            var meat = await meatRepository.GetAsync(id);
            if (meat == null)
                return ServiceResult<MeatDto>.Fail(Messages.ItemNotFound);

            return meat.Adapt<MeatDto>();
        }

        public async Task<ServiceResult<MeatDto>> UpdateAsync(Guid id, MeatDto dto)
        {
            var validationResult = await validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                ServiceResult<MeatDto>.Fail(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var meat = dto.Adapt<Meat>();
            meat.Id = id;

            var response = await meatRepository.UpdateAsync(meat);
            return response.Adapt<MeatDto>();
        }
    }
}
