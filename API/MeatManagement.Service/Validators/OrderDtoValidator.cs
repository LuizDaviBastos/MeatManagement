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

            RuleFor(o => o.CreatedAt)
                .NotNull().WithMessage("A data do pedido é obrigatória.");

            RuleFor(o => o.Items)
                .NotNull().WithMessage("O pedido deve ter pelo menos um item.")
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

            RuleFor(i => i.QuantityKg)
                .GreaterThan(0).WithMessage("A quantidade deve ser maior que zero.");

            RuleFor(i => i.PricePerKg)
                .GreaterThanOrEqualTo(0).WithMessage("O preço por kg deve ser maior ou igual a zero.");

            RuleFor(i => i.Total)
                .GreaterThanOrEqualTo(0).WithMessage("O total deve ser maior ou igual a zero.");
        }
    }

}
