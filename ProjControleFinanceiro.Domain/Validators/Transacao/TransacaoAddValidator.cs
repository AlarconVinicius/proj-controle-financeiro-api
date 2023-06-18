using FluentValidation;
using ProjControleFinanceiro.Domain.DTOs.Transacao;
using ProjControleFinanceiro.Entities.Entidades.Enums;

namespace ProjControleFinanceiro.Domain.Validators.Transacao
{
    public class TransacaoAddValidator : AbstractValidator<TransacaoAddDTO>
    {
        public TransacaoAddValidator()
        {
            RuleFor(dto => dto.ContaId)
                .NotEmpty().WithMessage("O campo ContaId é obrigatório.");

            RuleFor(dto => dto.MetodoPagamento)
                .NotEmpty().WithMessage("O campo MetodoPagamento é obrigatório.");

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

            When(dto => dto.MetodoPagamento == MetodoPagamento.CartaoCredito, () =>
            {
                RuleFor(dto => dto.CartaoId)
                    .NotEmpty().WithMessage("O campo CartaoId é obrigatório.");
            }).Otherwise(() =>
            {
                RuleFor(dto => dto.CartaoId)
                    .NotNull().WithMessage("O campo CartaoId não pode ser passado.")
                    .Empty().WithMessage("O campo CartaoId não pode ser passado.");
            });

            When(dto => dto.Repete, () =>
            {
                RuleFor(dto => dto.QtdRepeticao)
                    .NotEmpty().WithMessage("O campo QtdRepeticao é obrigatório.")
                    .GreaterThan(0).WithMessage("O campo QtdRepeticao deve ser maior que 0.");
            });
        }
    }
}