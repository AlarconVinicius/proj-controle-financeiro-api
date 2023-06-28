using FluentValidation;
using ProjControleFinanceiro.Domain.DTOs.Transacao;
using ProjControleFinanceiro.Domain.Extensions;
using ProjControleFinanceiro.Domain.Interfaces.Repositorios;
using ProjControleFinanceiro.Domain.Interfaces.Services;
using ProjControleFinanceiro.Entities.Entidades;

namespace ProjControleFinanceiro.Domain.Services
{
    public class TransacaoService : MainService, ITransacaoService
    {
        private readonly ITransacaoRepository _transacaoRepository;
        private readonly IValidator<TransacaoAddDTO> _addValidator;

        public TransacaoService(ITransacaoRepository transacaoRepository, IValidator<TransacaoAddDTO> addValidator)
        {
            _transacaoRepository = transacaoRepository;
            _addValidator = addValidator;
        }
        public async Task<TransacaoViewDTO> AdicionarTransacao(TransacaoAddDTO objeto)
        {
            var validationResult = await _addValidator.ValidateAsync(objeto);
            if (!validationResult.IsValid)
            {
                AdicionarErroProcessamento(validationResult);
                return null;
            }
            Transacao objetoMapeado = objeto.ToAddDTO();
            await _transacaoRepository.AddAsync(objetoMapeado);
            return objetoMapeado.ToGetDTO();
        }

        public async Task<TransacaoViewDTO> ObterTransacaoPorId(int id)
        {
            var transacao = await _transacaoRepository.ObterTransacaoPorId(id);
            if (transacao == null)
            {
                AdicionarErroProcessamento("Transação não encontrada.");
                return null;
            }
            TransacaoViewDTO objetoMapeadoView = transacao.ToGetDTO();
            return objetoMapeadoView;
        }

        public async Task<IEnumerable<TransacaoViewDTO>> ObterTransacoes()
        {
            IEnumerable<Transacao> objetosDb = await _transacaoRepository.ObterTransacoes();
            return objetosDb.Select(x => x.ToGetDTO());
        }

        //-----------------------------------------------------------------------------------------------------------------//
        //private async Task<TransacaoViewDTO> AdicionarTransacaoCartaoCredito(Transacao objetoMapeado)
        //{
        //    Cartao cartao = await _cartaoRepository.GetEntityByIdAsync(objetoMapeado.CartaoId ?? 0);
        //    Fatura fatura = await _faturaRepository.ObterFaturaPorCartaoMesAno(objetoMapeado.CartaoId ?? 0, objetoMapeado.Data.Month, objetoMapeado.Data.Year);

        //    if (cartao == null || fatura == null)
        //    {
        //        AdicionarErroProcessamento(cartao == null ? "Cartão inválido." : "Fatura não encontrada.");
        //        return null;
        //    }

        //    objetoMapeado.FaturaId = fatura.Id;
        //    fatura.AdicionarTransacao(objetoMapeado);

        //    cartao.SubtrairSaldo(objetoMapeado.Valor);
        //    objetoMapeado.Pago = false;
        //    await _transacaoRepository.AddAsync(objetoMapeado);
        //    return objetoMapeado.ToGetDTO();
        //}
        //private async Task<TransacaoViewDTO> AdicionarTransacaoOutrosMetodosPagamento(Transacao objetoMapeado, Conta conta)
        //{
        //    if (objetoMapeado.TipoTransacao == TipoTransacao.Receita)
        //    {
        //        conta.AdicionarSaldo(objetoMapeado.Valor);
        //    }
        //    else if (objetoMapeado.TipoTransacao == TipoTransacao.Despesa)
        //    {
        //        conta.SubtrairSaldo(objetoMapeado.Valor);
        //    }
        //    else
        //    {
        //        AdicionarErroProcessamento("Tipo de Transação inválido.");
        //        return null;
        //    }

        //    await _transacaoRepository.AddAsync(objetoMapeado);
        //    return objetoMapeado.ToGetDTO();
        //}
    }
}