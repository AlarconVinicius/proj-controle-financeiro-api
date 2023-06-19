using FluentValidation;
using ProjControleFinanceiro.Domain.DTOs.Cartao;
using ProjControleFinanceiro.Domain.Extensions;

namespace ProjControleFinanceiro.Domain.Validators.Cartao
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

            RuleFor(dto => dto.VencimentoData)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                    .WithMessage("A data de vencimento é obrigatório")
                .Matches(@"^\d{2}/\d{2}/\d{4}$")
                    .WithMessage("O campo Data deve seguir o padrão dd/MM/yyyy.")
                .Must((dto, vencimento) => vencimentoCorreto(vencimento, dto.FechamentoData))
                    .WithMessage("Data vencimento precisa ser maior que de fechamento");

            RuleFor(dto => dto.FechamentoData)
                .NotEmpty()
                    .WithMessage("A data de fechamento é obrigatório.")
                .Matches(@"^\d{2}/\d{2}/\d{4}$")
                    .WithMessage("O campo Data deve seguir o padrão dd/MM/yyyy.");
        }

        private bool vencimentoCorreto(string vencimento, string fechamento)
        {
            DateTime dtVencimento = vencimento.ToDateTime();
            DateTime dtFechamento = fechamento.ToDateTime();

            return dtVencimento >= dtFechamento ;
        }
    }
}
