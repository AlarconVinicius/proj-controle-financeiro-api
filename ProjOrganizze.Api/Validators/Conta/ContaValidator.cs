using FluentValidation;
using ProjOrganizze.Api.Dominio.DTOs.Conta;

namespace ProjOrganizze.Api.Validators.Conta
{
    public class ContaValidator : AbstractValidator<ContaUpdDTO>
    {
       
        public ContaValidator()
        {
            RuleFor(dto => dto.Nome)
           .NotEmpty().WithMessage("O campo nome é obrigatório.");

            RuleFor(dto => dto.TipoConta)
                 .NotEmpty().WithMessage("O campo tipo de conta é obrigatório");

            RuleFor(dto => dto.Saldo)
                .GreaterThan(0).WithMessage("O campo {PropertyName} precisa ser maior que {ComparisonValue}");




            //caso precise futuramente de algo mais complexo para TipoDeConta
            //When(dto => dto.TipoTransacao == Dominio.Entidades.Enums.TipoTransacao.CartaoDeCredito, () =>
            //{
            //    RuleFor(dto => dto.CartaoId)
            //       .NotEmpty().WithMessage("O campo CartaoId é obrigatório.");
            //})
            //When(dto => dto.TipoTransacao != Dominio.Entidades.Enums.TipoTransacao.CartaoDeCredito, () =>
            //{
            //    RuleFor(dto => dto.CartaoId)
            //       .Empty().WithMessage("O CartaoId não pode ser passado.");
            //})
            //When(dto => dto.Repetir != False, () =>
            //{
            //    RuleFor(dto => dto.QtdRepeticao)
            //       .NotEmpty().WithMessage("O campo CartaoId é obrigatório.");
            //       .GreaterThan(0).WithMessage("O QtdRepeticao deve ser maior que zero.");
            //})

        }

    }
}
