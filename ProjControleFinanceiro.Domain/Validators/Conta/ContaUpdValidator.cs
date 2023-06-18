using FluentValidation;
using ProjControleFinanceiro.Domain.DTOs.Conta;

namespace ProjControleFinanceiro.Domain.Validators.Conta
{
    public class ContaUpdValidator : AbstractValidator<ContaUpdDTO>
    {
       
        public ContaUpdValidator()
        {
            RuleFor(dto => dto.Nome)
           .NotEmpty().WithMessage("O campo nome é obrigatório.");

            RuleFor(dto => dto.TipoConta)
                 .NotEmpty().WithMessage("O campo tipo de conta é obrigatório");
        }

    }
}
