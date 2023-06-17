using FluentValidation;
using ProjOrganizze.Api.Dominio.DTOs.Conta;

namespace ProjOrganizze.Api.Validators.Conta
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
