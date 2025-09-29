using FluentValidation;
using MeatManager.Service.DTOs;

namespace MeatManager.Service.Validators
{
    public class OrderDtoValidator : AbstractValidator<OrderDto>
    {
        public OrderDtoValidator()
        {
            RuleFor(o => o.Buyer)
                .NotNull().WithMessage("O comprador é obrigatório.");

            RuleFor(o => o.OrderDate)
                .NotNull().WithMessage("A data do pedido é obrigatória.");

            RuleFor(o => o.Items)
                .NotNull().WithMessage("O pedido deve conter pelo menos um item.")
                .Must(items => items != null && items.Count > 0)
                .WithMessage("O pedido deve conter pelo menos um item.");

            RuleForEach(o => o.Items).SetValidator(new OrderItemDtoValidator());
        }
    }

    public class OrderItemDtoValidator : AbstractValidator<OrderItemDto>
    {
        public OrderItemDtoValidator()
        {
            RuleFor(i => i.CurrencyCode)
                .NotEmpty().WithMessage("O código da moeda é obrigatório.");

            RuleFor(i => i.Price)
                .GreaterThan(0).WithMessage("O preço deve ser maior que zero.");

            RuleFor(i => i.MeatId)
                .Must(id => !string.IsNullOrWhiteSpace(id))
                .WithMessage("O item deve ter um produto.");
        }
    }

}
