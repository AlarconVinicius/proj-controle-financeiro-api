using FluentValidation.Results;

namespace ProjControleFinanceiro.Domain.Interfaces.Services
{
    public interface IMainService
    {
        ICollection<string> GetErrors();
        void AdicionarErroProcessamento(string error);
        void AdicionarErroProcessamento(ValidationResult validation);
        bool OperacaoValida();
    }
}
