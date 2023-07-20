using FluentValidation.Results;
using ProjControleFinanceiro.Domain.Interfaces.Services;

namespace ProjControleFinanceiro.Domain.Services
{
    public class MainService : IMainService
    {
        protected ICollection<string> Errors = new List<string>();

        public ICollection<string> GetErrors()
        {
            return Errors;
        }
        public void AdicionarErroProcessamento(string error)
        {
            Errors.Add(error);
        }
        public void AdicionarErroProcessamento(List<string> errors)
        {
            foreach ( var error in errors)
            {
                Errors.Add(error);
            }
        }
        public void AdicionarErroProcessamento(ValidationResult validation)
        {
            var errors = validation.Errors;
            foreach (var error in errors)
            {
                Errors.Add(error.ErrorMessage);
            }
        }

        public bool OperacaoValida()
        {
            return !Errors.Any();
        }
    }
}
