using FluentValidation;
using ProjOrganizze.Api.Dominio.DTOs.Cartao;
using ProjOrganizze.Api.Extensions;

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

            RuleFor(dto => dto.VencimentoData)
                .Must((dto, vencimento)=> vencimentoCorreto(vencimento, dto.FechamentoData))
                .WithMessage("Data vencimento precisa ser maior que de fechamento")
                .Matches(@"^\d{2}/\d{2}/\d{4}$")
                .WithMessage("O campo Data deve seguir o padrão dd/MM/yyyy.")
                .NotEmpty().WithMessage("A data de vencimento é obrigatório");

            RuleFor(dto => dto.FechamentoData)
                .Matches(@"^\d{2}/\d{2}/\d{4}$")
                .WithMessage("O campo Data deve seguir o padrão dd/MM/yyyy.")
                .NotEmpty().WithMessage("A data de fechamento é obrigatório.");

        }

        private bool vencimentoCorreto(string vencimento, string fechamento)
        {
            //converter para data 
            DateTime dtVencimento = vencimento.ToDateTime();
            DateTime dtFechamento = fechamento.ToDateTime();

             return dtVencimento >= dtFechamento ;

        }
    }
}
