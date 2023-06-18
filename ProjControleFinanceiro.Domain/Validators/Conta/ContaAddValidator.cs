using FluentValidation;
using ProjControleFinanceiro.Domain.DTOs.Conta;

namespace ProjControleFinanceiro.Domain.Validators.Conta
{
    public class ContaAddValidator : AbstractValidator<ContaAddDTO>
    {
       
        public ContaAddValidator()
        {
            RuleFor(dto => dto.Nome)
           .NotEmpty().WithMessage("O campo nome é obrigatório.");

            RuleFor(dto => dto.TipoConta)
                 .NotEmpty().WithMessage("O campo tipo de conta é obrigatório");

            RuleFor(dto => dto.Saldo)
                .GreaterThan(0).WithMessage("O campo {PropertyName} precisa ser maior que {ComparisonValue}");
        }

    }
}
