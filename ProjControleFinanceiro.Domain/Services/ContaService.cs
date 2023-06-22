using FluentValidation;
using ProjControleFinanceiro.Domain.DTOs.Conta;
using ProjControleFinanceiro.Domain.Extensions;
using ProjControleFinanceiro.Domain.Interfaces.Repositorios;
using ProjControleFinanceiro.Domain.Interfaces.Services;
using ProjControleFinanceiro.Entities.Entidades;

namespace ProjControleFinanceiro.Domain.Services
{
    public class ContaService : MainService, IContaService
    {
        private readonly IContaRepository _contaRepository;
        private readonly IValidator<ContaAddDTO> _addValidator;
        private readonly IValidator<ContaUpdDTO> _updValidator;

        public ContaService(IContaRepository contaRepository, IValidator<ContaAddDTO> addValidator, IValidator<ContaUpdDTO> updValidator)
        {
            _contaRepository = contaRepository;
            _addValidator = addValidator;
            _updValidator = updValidator;
        }


        public async Task<ContaViewDTO> AdicionarConta(ContaAddDTO objeto)
        {
            var validationResult = await _addValidator.ValidateAsync(objeto);
            if (!validationResult.IsValid)
            {
                AdicionarErroProcessamento(validationResult);
                return null;
            }
            Conta objetoMapeado = objeto.ToAddDTO();
            if (await NomeExiste(objetoMapeado.Nome))
            {
                return null;
            }
            await _contaRepository.AddAsync(objetoMapeado);
            var objetoMapeadoView = objetoMapeado.ToGetDTO();
            return objetoMapeadoView;
        }

        public async Task<ContaViewDTO> AtualizarConta(ContaUpdDTO objeto)
        {
            var validationResult = await _updValidator.ValidateAsync(objeto);
            if (!validationResult.IsValid)
            {
                AdicionarErroProcessamento(validationResult);
                return null;
            }
            if (!await ContaExiste(objeto.Id))
            {
                return null;
            };
            var objetoDb = await _contaRepository.GetEntityByIdAsync(objeto.Id);
            if (objetoDb.Nome != objeto.Nome)
            {
                if (await NomeExiste(objeto.Nome))
                {
                    return null;
                }
            }
            objetoDb.Nome = objeto.Nome;
            objetoDb.TipoConta = objeto.TipoConta;
            await _contaRepository.UpdateAsync(objetoDb);

            return objetoDb.ToGetDTO();
        }

        public async Task DeletarConta(int id)
        {
            if (!await ContaExiste(id))
            {
                return;
            };
            await _contaRepository.DeleteAsync(id);
        }

        public async Task<ContaViewDTO> ObterContaPorId(int id)
        {
            if(!await ContaExiste(id))
            {
                return null;
            };
            var objetoDb = await _contaRepository.GetEntityByIdAsync(id);
            return objetoDb.ToGetDTO();
        }

        public async Task<IEnumerable<ContaViewDTO>> ObterContas()
        {
            IEnumerable<Conta> objetosDb = await _contaRepository.ListAsync();;
            return objetosDb.Select(x => x.ToGetDTO());
        }
        private async Task<bool> ContaExiste(int id)
        {
            var objetoDb = await _contaRepository.GetEntityByIdAsync(id);
            if (objetoDb == null)
            {
                AdicionarErroProcessamento("Conta não encontrada.");
                return false;
            }
            return true;
        }
        private async Task<bool> NomeExiste(string nome)
        {
            var objetosDb = await _contaRepository.ListAsync();
            if (objetosDb.Any(p => p.Nome == nome))
            {
                AdicionarErroProcessamento("Não é possível adicionar mais de uma conta com o mesmo nome.");
                return true;
            }
            return false;
        }
    }
}