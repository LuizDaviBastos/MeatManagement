using FluentValidation;
using MeatManager.Service.DTOs;

namespace MeatManager.Service.Validators
{
    public class MeatDtoValidator : AbstractValidator<MeatDto>
    {
        public MeatDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome do produto é obrigatório.")
                .MaximumLength(100);

            RuleFor(x => x.PricePerKg)
                .GreaterThan(0).WithMessage("O preço por kg deve ser maior que zero.");

            RuleFor(x => x.WeightKg)
                .GreaterThan(0).WithMessage("O peso deve ser maior que zero.");
        }
    }
}
