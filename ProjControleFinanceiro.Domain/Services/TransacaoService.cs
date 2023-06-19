﻿using ProjControleFinanceiro.Domain.Exceptions;
using ProjControleFinanceiro.Domain.Interfaces.Repositorios;
using ProjControleFinanceiro.Domain.Interfaces.Services;
using ProjControleFinanceiro.Entities.Entidades;
using ProjControleFinanceiro.Entities.Entidades.Enums;
using ProjControleFinanceiro.Entities.Filtros;

namespace ProjControleFinanceiro.Domain.Services
{
    public class TransacaoService : ITransacaoService
    {
        private readonly ICartaoRepository _cartaoRepository;
        private readonly IContaRepository _contaRepository;
        private readonly IFaturaRepository _faturaRepository;
        private readonly ITransacaoRepository _transacaoRepository;

        public TransacaoService(ICartaoRepository cartaoRepository, IContaRepository contaRepository, IFaturaRepository faturaRepository, ITransacaoRepository transacaoRepository)
        {
            _cartaoRepository = cartaoRepository;
            _contaRepository = contaRepository;
            _faturaRepository = faturaRepository;
            _transacaoRepository = transacaoRepository;
        }
        public async Task<Transacao> AdicionarTransacao(Transacao objeto)
        {
            Conta conta = await _contaRepository.GetEntityByIdAsync(objeto.ContaId);
            if (conta == null)
            {
                throw new ServiceException("Conta inválida.");
            }
            if (objeto.MetodoPagamento.Equals(MetodoPagamento.CartaoCredito))
            {
                Cartao cartao = await _cartaoRepository.GetEntityByIdAsync(objeto.CartaoId ?? 0);
                if (cartao == null)
                {
                    throw new ServiceException("Cartão inválido.");
                }
                Fatura fatura = await _faturaRepository.ObterFaturaPorCartaoMesAno(objeto.CartaoId ?? 0, objeto.Data.Month, objeto.Data.Year);
                if (fatura == null)
                {
                    throw new ServiceException("Fatura não encontrada.");
                }
                objeto.FaturaId = fatura.Id;
                fatura.AdicionarTransacao(objeto);

                cartao.SubtrairSaldo(objeto.Valor);
                objeto.Pago = false;
                // Talvez parar de salvar a transação aqui e deixar somente na fatura
                await _transacaoRepository.AddAsync(objeto);
                return objeto;
            }
            if (objeto.CartaoId.HasValue && objeto.CartaoId > 0)
            {
                throw new ServiceException("CartaoId não deve ser passado nesse tipo de MetodoPagamento.");
            }

            if (objeto.TipoTransacao == TipoTransacao.Receita)
            {
                conta.AdicionarSaldo(objeto.Valor);
            }
            else if (objeto.TipoTransacao == TipoTransacao.Despesa)
            {
                //if (objeto.Valor > conta.Saldo)
                //{
                //    throw new ServiceException("Saldo insuficiente na conta.");
                //}
                conta.SubtrairSaldo(objeto.Valor);
            }
            else
            {
                throw new ServiceException("Tipo de Transação inválido.");
            }

            await _transacaoRepository.AddAsync(objeto);
            return objeto;
        }

        public async Task<Transacao> ObterTransacaoPorId(int id)
        {
            var transacao = await _transacaoRepository.ObterTransacaoPorId(id);
            if (transacao == null)
            {
                throw new ServiceException("Transação inválida.");
            }
            return transacao;
        }

        public async Task<List<Transacao>> ObterTransacoes(TransacaoFiltro filtro)
        {
            return await _transacaoRepository.ObterTransacoes(filtro);
        }
    }
}