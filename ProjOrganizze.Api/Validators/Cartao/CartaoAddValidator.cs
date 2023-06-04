using FluentValidation;
using ProjOrganizze.Api.Dominio.DTOs.Cartao;

namespace ProjOrganizze.Api.Validators.Cartao
{
    public class CartaoAddValidator : AbstractValidator<CartaoAddDTO>
    {
        public CartaoAddValidator()
        {
            RuleFor(dto => dto.Nome)
            .NotEmpty().WithMessage("O nome é obrigatório.");

            RuleFor(dto => dto.Limite)
                .NotEmpty().WithMessage("O limite é obrigatório.")
                .GreaterThan(0).WithMessage("O limite deve ser maior que zero.");

            RuleFor(dto => dto.VencimentoMes)
                .NotEmpty().WithMessage("O mês de vencimento é obrigatório.")
                .InclusiveBetween(1, 12).WithMessage("O mês de vencimento deve estar entre 1 e 12.");

            RuleFor(dto => dto.VencimentoDia)
                .NotEmpty().WithMessage("O dia de vencimento é obrigatório.")
                .InclusiveBetween(1, 31).WithMessage("O dia de vencimento deve estar entre 1 e 31.");

            RuleFor(dto => dto.FechamentoMes)
                .NotEmpty().WithMessage("O mês de fechamento é obrigatório.")
                .InclusiveBetween(1, 12).WithMessage("O mês de fechamento deve estar entre 1 e 12.");

            RuleFor(dto => dto.FechamentoDia)
                .NotEmpty().WithMessage("O dia de fechamento é obrigatório.")
                .InclusiveBetween(1, 31).WithMessage("O dia de fechamento deve estar entre 1 e 31.");
        }
    }
}
