using ProjOrganizze.Api.Banco.Repositorios;
using ProjOrganizze.Api.Dominio.DTOs.Fatura;
using ProjOrganizze.Api.Dominio.Entidades;
using ProjOrganizze.Api.Dominio.Entidades.Enums;
using ProjOrganizze.Api.Dominio.Interfaces.Repositorios;
using ProjOrganizze.Api.Dominio.Interfaces.Services;
using ProjOrganizze.Api.Exceptions;
using ProjOrganizze.Api.Mapeamentos;

namespace ProjOrganizze.Api.Services
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

            //transacao.Pago = true; // Por padrão, transações de conta são marcadas como pagas

            await _transacaoRepository.AddAsync(objeto);
            return objeto;
        }
    }
}
