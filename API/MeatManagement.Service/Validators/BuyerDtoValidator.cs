using FluentValidation;
using MeatManager.Service.DTOs;

namespace MeatManager.Service.Validators
{
    public class BuyerDtoValidator : AbstractValidator<BuyerDto>
    {
        public BuyerDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome do comprador é obrigatório")
                .MaximumLength(150).WithMessage("O nome do comprador não pode exceder 150 caracteres");

            RuleFor(x => x.Document)
                .NotEmpty()
                .WithMessage("O documento do comprador é obrigatório")
                .MinimumLength(11).WithMessage("O documento do comprador é obrigatório")
                .MaximumLength(20).WithMessage("O documento do comprador não pode exceder 20 caracteres");
        }
    }
}
