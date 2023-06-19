using FluentValidation;
using ProjControleFinanceiro.Domain.DTOs.Cartao;
using ProjControleFinanceiro.Domain.Extensions;
using ProjControleFinanceiro.Domain.Interfaces.Repositorios;
using ProjControleFinanceiro.Domain.Interfaces.Services;
using ProjControleFinanceiro.Entities.Entidades;

namespace ProjControleFinanceiro.Domain.Services
{
    public class CartaoService : MainService, ICartaoService
    {
        private readonly ICartaoRepository _cartaoRepository;
        private readonly IContaRepository _contaRepository;
        private readonly IFaturaRepository _faturaRepository;
        private readonly IValidator<CartaoAddDTO> _addValidator;
        private readonly IValidator<CartaoUpdDTO> _updValidator;
        public CartaoService(ICartaoRepository cartaoRepository, IContaRepository contaRepository, IFaturaRepository faturaRepository, IValidator<CartaoAddDTO> addValidator, IValidator<CartaoUpdDTO> updValidator)
        {
            _cartaoRepository = cartaoRepository;
            _contaRepository = contaRepository;
            _faturaRepository = faturaRepository;
            _addValidator = addValidator;
            _updValidator = updValidator;
        }

        public async Task<CartaoViewDTO> AdicionarCartao(CartaoAddDTO objeto)
        {
            var validationResult = await _addValidator.ValidateAsync(objeto);
            if (!validationResult.IsValid)
            {
                AdicionarErroProcessamento(validationResult);
                return null;
            }
            Cartao objetoMapeado = objeto.ToAddDTO();
            var contaExiste = await _contaRepository.GetEntityByIdAsync(objetoMapeado.ContaId);
            var cartoesDb = await _cartaoRepository.ObterCartoes();
            if (contaExiste == null)
            {
                AdicionarErroProcessamento("Conta inválida.");
                return null;
            }
            if (await NomeExiste(objeto.Nome))
            {
                return null;
            }
            await _cartaoRepository.AddAsync(objetoMapeado);
            await _faturaRepository.AdicionarFaturas(objetoMapeado);
            //// Adicionar await _unitOfWorkRepository.Commit();
            var objetoMapeadoView = objetoMapeado.ToGetDTO();
            return objetoMapeadoView;
        }

        public async Task<CartaoViewDTO> AtualizarCartao(CartaoUpdDTO objeto)
        {
            if (await CartaoExiste(objeto.Id))
            {
                return null;
            }
            var validationResult = await _updValidator.ValidateAsync(objeto);
            if (!validationResult.IsValid)
            {
                AdicionarErroProcessamento(validationResult);
                return null;
            }
            var objetoDb = await _cartaoRepository.GetEntityByIdAsync(objeto.Id);
            if (objetoDb.Nome != objeto.Nome)
            {
                if (await NomeExiste(objeto.Nome))
                {
                    return null;
                }
            }
            objetoDb.Nome = objeto.Nome;
            objetoDb.Limite = objeto.Limite;
            objetoDb.DiaVencimento = objeto.VencimentoDia;
            await _cartaoRepository.UpdateAsync(objetoDb);
            return objetoDb.ToGetDTO();
        }

        public async Task DeletarCartao(int id)
        {
            if (await CartaoExiste(id))
            {
                return;
            }
            await _cartaoRepository.DeletarCartao(id);
        }

        public async Task<CartaoViewDTO> ObterCartaoPorId(int id)
        {
            if (await CartaoExiste(id))
            {
                return null;
            }
            var objetoDb = await _cartaoRepository.ObterCartaoPorId(id);
            return objetoDb.ToGetDetailsDTO();
        }

        public async Task<IEnumerable<CartaoViewDTO>> ObterCartoes()
        {
            IEnumerable<Cartao> objetosDb = await _cartaoRepository.ObterCartoes();
            return objetosDb.Select(x => x.ToGetDTO());
        }

        private async Task<bool> CartaoExiste(int id)
        {
            var objetoDb = await _cartaoRepository.GetEntityByIdAsync(id);
            if (objetoDb == null)
            {
                AdicionarErroProcessamento("Cartão não encontrado.");
                return true;
            }
            return false;
        }
        private async Task<bool> NomeExiste(string nome)
        {
            var cartoesDb = await _cartaoRepository.ObterCartoes();
            if (cartoesDb.Any(p => p.Nome == nome))
            {
                AdicionarErroProcessamento("Não é possível adicionar mais de um cartão com o mesmo nome.");
                return true;
            }
            return false;
        }
    }
}
