using FluentValidation;
using MeatManager.Model.Entities;
using MeatManager.Model.Interfaces;
using MeatManager.Service.DTOs;
using MeatManager.Service.Interfaces;
using MeatManager.Service.Resources;
using IMapper = AutoMapper.IMapper;

namespace MeatManager.Service.Services
{
    public class MeatService : IMeatService
    {
        private readonly IMeatRepository meatRepository;
        private readonly IValidator<MeatDto> validator;
        private readonly IMapper mapper;

        public MeatService(IMeatRepository meatRepository, IValidator<MeatDto> validator, IMapper mapper)
        {
            this.meatRepository = meatRepository;
            this.validator = validator;
            this.mapper = mapper;
        }

        public async Task<ServiceResult<MeatDto>> CreateAsync(MeatDto dto)
        {
            var validationResult = await validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return ServiceResult<MeatDto>.Fail(validationResult.Errors.Select(e => e.ErrorMessage));

            var meat = mapper.Map<Meat>(dto);
            meat.CreatedAt = DateTime.UtcNow;

            var response = await meatRepository.SaveAsync(meat);
            return mapper.Map<MeatDto>(response);
        }

        public async Task<ServiceResult<bool>> DeleteAsync(Guid id)
        {
            var exists = await meatRepository.ExistsAsync(id);
            if (!exists)
                return ServiceResult<bool>.Fail(Messages.ItemNotFound);

            var hasOrders = await meatRepository.HasOrdersAsync(id);
            if (hasOrders)
                return ServiceResult<bool>.Fail(Messages.MeatHasOrders);

            var deleted = await meatRepository.DeleteAsync(id);
            if (deleted) return true;

            return ServiceResult<bool>.Fail(Messages.DeleteItemFailed);
        }

        public async Task<ServiceResult<IEnumerable<MeatDto>>> GetAllAsync()
        {
            var meats = await meatRepository.GetAllAsync();
            var meatDtos = mapper.Map<IEnumerable<MeatDto>>(meats);
            return ServiceResult<IEnumerable<MeatDto>>.Ok(meatDtos);
        }

        public async Task<ServiceResult<MeatDto>> GetByIdAsync(Guid id)
        {
            var meat = await meatRepository.GetAsync(id);
            if (meat == null)
                return ServiceResult<MeatDto>.Fail(Messages.ItemNotFound);

            return mapper.Map<MeatDto>(meat);
        }

        public async Task<ServiceResult<MeatDto>> UpdateAsync(Guid id, MeatDto dto)
        {
            var exists = await meatRepository.ExistsAsync(id);
            if (!exists)
                return ServiceResult<MeatDto>.Fail(Messages.ItemNotFound, ServiceError.NotFound);

            var validationResult = await validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return ServiceResult<MeatDto>.Fail(validationResult.Errors.Select(e => e.ErrorMessage));

            var meat = mapper.Map<Meat>(dto);
            meat.Id = id;

            var response = await meatRepository.UpdateAsync(meat);
            return mapper.Map<MeatDto>(response);
        }
    }
}
