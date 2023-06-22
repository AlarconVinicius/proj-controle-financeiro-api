using FluentValidation;
using ProjControleFinanceiro.Domain.DTOs.Transacao;
using ProjControleFinanceiro.Domain.Extensions;
using ProjControleFinanceiro.Domain.Interfaces.Repositorios;
using ProjControleFinanceiro.Domain.Interfaces.Services;
using ProjControleFinanceiro.Entities.Entidades;
using ProjControleFinanceiro.Entities.Entidades.Enums;
using ProjControleFinanceiro.Entities.Filtros;

namespace ProjControleFinanceiro.Domain.Services
{
    public class TransacaoService : MainService, ITransacaoService
    {
        private readonly ICartaoRepository _cartaoRepository;
        private readonly IContaRepository _contaRepository;
        private readonly IFaturaRepository _faturaRepository;
        private readonly ITransacaoRepository _transacaoRepository;
        private readonly IValidator<TransacaoAddDTO> _addValidator;

        public TransacaoService(ICartaoRepository cartaoRepository, IContaRepository contaRepository, IFaturaRepository faturaRepository, ITransacaoRepository transacaoRepository, IValidator<TransacaoAddDTO> addValidator)
        {
            _cartaoRepository = cartaoRepository;
            _contaRepository = contaRepository;
            _faturaRepository = faturaRepository;
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
            Transacao objetoMapeado;
            TransacaoViewDTO objetoMapeadoView;
            if (objeto.CartaoId > 0 || objeto.CartaoId != 0)
            {
                objetoMapeado = objeto.ToAddDTO();
            }
            else
            {
                objetoMapeado = objeto.ToAddDTO2();
            }
            Conta conta = await _contaRepository.GetEntityByIdAsync(objetoMapeado.ContaId);
            if (conta == null)
            {
                AdicionarErroProcessamento("Conta inválida.");
                return null;
            }
            if (objetoMapeado.MetodoPagamento.Equals(MetodoPagamento.CartaoCredito))
            {
                Cartao cartao = await _cartaoRepository.GetEntityByIdAsync(objetoMapeado.CartaoId ?? 0);
                if (cartao == null)
                {
                    AdicionarErroProcessamento("Cartão inválido.");
                    return null;
                }
                Fatura fatura = await _faturaRepository.ObterFaturaPorCartaoMesAno(objetoMapeado.CartaoId ?? 0, objetoMapeado.Data.Month, objetoMapeado.Data.Year);
                if (fatura == null)
                {
                    AdicionarErroProcessamento("Fatura não encontrada.");
                    return null;
                }
                objetoMapeado.FaturaId = fatura.Id;
                fatura.AdicionarTransacao(objetoMapeado);

                cartao.SubtrairSaldo(objeto.Valor);
                objeto.Pago = false;
                // Talvez parar de salvar a transação aqui e deixar somente na fatura
                await _transacaoRepository.AddAsync(objetoMapeado);
                objetoMapeadoView = objetoMapeado.ToGetDTO();
                return objetoMapeadoView;
            }
            if (objetoMapeado.CartaoId.HasValue && objetoMapeado.CartaoId > 0)
            {
                AdicionarErroProcessamento("CartaoId não deve ser passado nesse tipo de MetodoPagamento.");
                return null;
            }

            if (objetoMapeado.TipoTransacao == TipoTransacao.Receita)
            {
                conta.AdicionarSaldo(objetoMapeado.Valor);
            }
            else if (objetoMapeado.TipoTransacao == TipoTransacao.Despesa)
            {
                //if (objeto.Valor > conta.Saldo)
                //{
                //    throw new ServiceException("Saldo insuficiente na conta.");
                //}
                conta.SubtrairSaldo(objeto.Valor);
            }
            else
            {
                AdicionarErroProcessamento("Tipo de Transação inválido.");
                return null;
            }

            await _transacaoRepository.AddAsync(objetoMapeado);
            objetoMapeadoView = objetoMapeado.ToGetDTO();
            return objetoMapeadoView;
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

        public async Task<IEnumerable<TransacaoViewDTO>> ObterTransacoes(TransacaoFiltro filtro)
        {
            IEnumerable<Transacao> objetosDb = await _transacaoRepository.ObterTransacoes(filtro);
            return objetosDb.Select(x => x.ToGetDTO());
        }
    }
}