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
        public class OrderService : IOrderService
        {
            private readonly IOrderRepository orderRepository;
            private readonly ICurrencyConversionService currencyConversionService;
            private readonly IValidator<OrderDto> validator;

            public OrderService(IOrderRepository orderRepository, ICurrencyConversionService currencyConversionService, IValidator<OrderDto> validator)
            {
                this.orderRepository = orderRepository;
                this.validator = validator;
                this.currencyConversionService = currencyConversionService;
            }

            public async Task<ServiceResult<OrderDto>> CreateAsync(OrderDto dto)
            {
                var validationResult = await validator.ValidateAsync(dto);
                if (!validationResult.IsValid)
                    return ServiceResult<OrderDto>.Fail(validationResult.Errors.Select(e => e.ErrorMessage));

                var order = dto.Adapt<Order>();
                order.CreatedAt = DateTime.UtcNow;

                var response = await orderRepository.SaveAsync(order);
                return response.Adapt<OrderDto>();
            }

            public async Task<ServiceResult<bool>> DeleteAsync(Guid id)
            {
                var deleted = await orderRepository.DeleteAsync(id);
                if (deleted)
                    return ServiceResult<bool>.Ok(true);

                return ServiceResult<bool>.Fail(Messages.DeleteItemFailed);
            }

            public async Task<ServiceResult<IEnumerable<OrderDto>>> GetAllAsync()
            {
                var orders = await orderRepository.GetAllAsync();
                var orderDtos = orders.Adapt<IEnumerable<OrderDto>>();

                foreach (var item in orderDtos.SelectMany(o => o.Items))
                {
                    item.TotalBRL = await currencyConversionService
                            .ConvertToBRLAsync(item.Total, item.CurrencyCode);
                }

                return ServiceResult<IEnumerable<OrderDto>>.Ok(orderDtos);
            }

            public async Task<ServiceResult<OrderDto>> GetByIdAsync(Guid id)
            {
                var order = await orderRepository.GetAsync(id);
                if (order == null)
                    return ServiceResult<OrderDto>.Fail(Messages.ItemNotFound);

                return order.Adapt<OrderDto>();
            }

            public async Task<ServiceResult<OrderDto>> UpdateAsync(Guid id, OrderDto dto)
            {
                var validationResult = await validator.ValidateAsync(dto);
                if (!validationResult.IsValid)
                    return ServiceResult<OrderDto>.Fail(validationResult.Errors.Select(e => e.ErrorMessage));

                var order = dto.Adapt<Order>();
                order.Id = id;

                var response = await orderRepository.UpdateAsync(order);
                return response.Adapt<OrderDto>();
            }
        }
    }

}
