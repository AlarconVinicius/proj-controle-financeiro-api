using FluentValidation;
using ProjOrganizze.Api.Dominio.DTOs.Cartao;

namespace ProjOrganizze.Api.Validators.Cartao
{
    public class CartaoUpdValidator : AbstractValidator<CartaoUpdDTO>
    {
        public CartaoUpdValidator()
        {
            RuleFor(dto => dto.Nome)
            .NotEmpty().WithMessage("O nome é obrigatório.");

            RuleFor(dto => dto.Limite)
                .NotEmpty().WithMessage("O limite é obrigatório.")
                .GreaterThan(0).WithMessage("O limite deve ser maior que zero.");

            RuleFor(dto => dto.VencimentoDia)
                .NotEmpty().WithMessage("O dia de vencimento é obrigatório.")
                .InclusiveBetween(1, 31).WithMessage("O dia de vencimento deve estar entre 1 e 31.");

        }
    }
}
