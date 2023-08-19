using FluentValidation;
using ProjControleFinanceiro.Domain.DTOs.Transacao;

namespace ProjControleFinanceiro.Domain.Validators.Transacao;

public class TransacaoUpdValidator : AbstractValidator<TransacaoUpdDto>
{
    public TransacaoUpdValidator()
    {
        RuleFor(dto => dto.Descricao)
            .NotEmpty().WithMessage("O campo Descricao é obrigatório.");

        RuleFor(dto => dto.Valor)
            .NotEmpty().WithMessage("O campo Valor é obrigatório.")
            .GreaterThan(0).WithMessage("O campo Valor deve ser maior que 0.");

        RuleFor(dto => dto.Data)
            .NotEmpty()
                .WithMessage("A data é obrigatória.")
            .Matches(@"^\d{2}/\d{2}/\d{4}$")
                .WithMessage("O campo Data deve seguir o padrão dd/MM/yyyy.");

        RuleFor(dto => dto.TipoTransacao)
            .NotEmpty().WithMessage("O campo TipoTransacao é obrigatório.");

        RuleFor(dto => dto.Categoria)
            .NotEmpty().WithMessage("O campo Categoria é obrigatório.");
    }
}