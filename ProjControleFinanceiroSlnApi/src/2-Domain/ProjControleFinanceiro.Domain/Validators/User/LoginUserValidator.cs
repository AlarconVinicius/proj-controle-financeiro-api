using FluentValidation;

using ProjControleFinanceiro.Domain.DTOs.Usuario;

namespace ProjControleFinanceiro.Domain.Validators.User;
public class LoginUserValidator : AbstractValidator<LoginUserRequest>
{
    public LoginUserValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
            .EmailAddress().WithMessage("O campo {PropertyName} está em formato inválido");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
            .Length(6, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
    }
}