using FluentValidation;

using ProjControleFinanceiro.Domain.DTOs.Usuario;

namespace ProjControleFinanceiro.Domain.Validators.User;
public class AddUserValidator : AbstractValidator<AddUserRequest>
{
    public AddUserValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
            .Length(3, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
            .Length(3, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
            .Length(8, 30).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
            .EmailAddress().WithMessage("O campo {PropertyName} está em formato inválido");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
            .Length(6, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password).WithMessage("As senhas não conferem");
    }
}