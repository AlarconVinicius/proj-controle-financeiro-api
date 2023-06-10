using FluentValidation;
using ProjOrganizze.Api.Dominio.DTOs.Cartao;
using ProjOrganizze.Api.Dominio.DTOs.Conta;

namespace ProjOrganizze.Api.Validators.Conta
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




            //caso precise futuramente de algo mais complexo para TipoDeConta
            //When(dto => dto.TipoConta == Dominio.Entidades.Enums.TipoConta.Corrente, () =>
            //{
            //    //Rule 
            //})

        }

    }
}
