using FluentValidation;
using MeatManager.Model.Entities;
using MeatManager.Model.Interfaces;
using MeatManager.Service.DTOs;
using MeatManager.Service.Interfaces;
using MeatManager.Service.Resources;
using IMapper = AutoMapper.IMapper;

namespace MeatManager.Service.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly ICurrencyConversionService currencyConversionService;
        private readonly IValidator<OrderDto> validator;
        private readonly IMapper mapper;

        public OrderService(IOrderRepository orderRepository, ICurrencyConversionService currencyConversionService, IValidator<OrderDto> validator, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.currencyConversionService = currencyConversionService;
            this.validator = validator;
            this.mapper = mapper;
        }

        public async Task<ServiceResult<OrderDto>> CreateAsync(OrderDto dto)
        {
            var validationResult = await validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return ServiceResult<OrderDto>.Fail(validationResult.Errors.Select(e => e.ErrorMessage));

            var order = mapper.Map<Order>(dto);
            var response = await orderRepository.SaveAsync(order);
            return mapper.Map<OrderDto>(response);
        }

        public async Task<ServiceResult<bool>> DeleteAsync(Guid id)
        {
            var exists = await orderRepository.ExistsAsync(id);
            if (!exists)
                return ServiceResult<bool>.Fail(Messages.ItemNotFound);

            var deleted = await orderRepository.DeleteAsync(id);
            if (deleted) return true;

            return ServiceResult<bool>.Fail(Messages.DeleteItemFailed);
        }

        public async Task<ServiceResult<IEnumerable<OrderDto>>> GetAllAsync()
        {
            var orders = await orderRepository.GetAllAsync();
            var orderDtos = mapper.Map<IEnumerable<OrderDto>>(orders);

            foreach (var order in orderDtos)
            {
                foreach (var item in order.Items)
                {
                    item.PriceBRL = await currencyConversionService.ConvertToBRLAsync(item.Price ?? 0, item.CurrencyCode);
                }

                order.Total = order.Items.Sum(i => i.Price ?? 0);
                order.TotalBRL = order.Items.Sum(i => i.PriceBRL);
            }

            return ServiceResult<IEnumerable<OrderDto>>.Ok(orderDtos);
        }

        public async Task<ServiceResult<OrderDto>> GetByIdAsync(Guid id)
        {
            var order = await orderRepository.GetAsync(id);
            if (order == null)
                return ServiceResult<OrderDto>.Fail(Messages.ItemNotFound);

            return mapper.Map<OrderDto>(order);
        }

        public async Task<ServiceResult<OrderDto>> UpdateAsync(Guid id, OrderDto dto)
        {
            var exists = await orderRepository.ExistsAsync(id);
            if (!exists)
                return ServiceResult<OrderDto>.Fail(Messages.ItemNotFound, ServiceError.NotFound);

            var validationResult = await validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return ServiceResult<OrderDto>.Fail(validationResult.Errors.Select(e => e.ErrorMessage));

            var order = mapper.Map<Order>(dto);
            order.Id = id;

            var response = await orderRepository.UpdateAsync(order);
            return mapper.Map<OrderDto>(response);
        }
    }
}