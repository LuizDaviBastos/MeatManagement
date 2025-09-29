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

        }
    }
}
