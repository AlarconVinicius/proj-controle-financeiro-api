using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ProjOrganizze.Api.Dominio.DTOs.Fatura;
using ProjOrganizze.Api.Dominio.DTOs.Transacao;
using ProjOrganizze.Api.Dominio.Entidades;
using ProjOrganizze.Api.Dominio.Entidades.Enums;
using ProjOrganizze.Api.Dominio.Interfaces.Repositorios;
using ProjOrganizze.Api.Extensions;
using ProjOrganizze.Api.Mapeamentos;

namespace ProjOrganizze.Api.Controllers
{
    [ApiController]
    [Route("api/transacoes")]
    public class TransacaoController : MainController
    {
        private readonly IBaseRepository<Conta> _contaRepository;
        private readonly IBaseRepository<Cartao> _cartaoRepository;
        private readonly ITransacaoRepository _transacaoRepository;
        private readonly IFaturaRepository _faturaRepository;
        private readonly TransacaoMapping _transacaoMapping;
        private readonly FaturaMapping _faturaMapping;
        private readonly IValidator<TransacaoAddDTO> _addValidator;
        public TransacaoController(IBaseRepository<Conta> contaRepository, ITransacaoRepository transacaoRepository, IBaseRepository<Cartao> cartaoRepository, IFaturaRepository faturaRepository, IValidator<TransacaoAddDTO> addValidator)
        {
            _contaRepository = contaRepository;
            _transacaoRepository = transacaoRepository;
            _cartaoRepository = cartaoRepository;
            _faturaRepository = faturaRepository;
            _transacaoMapping = new TransacaoMapping();
            _faturaMapping = new FaturaMapping();
            _addValidator = addValidator;
        }

        // Rota para adicionar uma nova transação
        [HttpPost]
        public async Task<IActionResult> AdicionarTransacao(TransacaoAddDTO transacaoDto)
        {
            var validationResult = await _addValidator.ValidateAsync(transacaoDto);
            if (!validationResult.IsValid) return CustomResponse(validationResult);
            Transacao transacao = transacaoDto.ToAddDTO();
            if (transacao.ContaId <= 0)
            {
                return BadRequest("Informe o ID da conta.");
            }
            if (transacao.MetodoPagamento == MetodoPagamento.CartaoCredito)
            {
                if (!transacao.CartaoId.HasValue || transacao.CartaoId <= 0)
                {
                    return BadRequest("Informe o ID do cartão de crédito.");
                }

                Cartao cartao = await _cartaoRepository.GetEntityByIdAsync(transacao.CartaoId ?? 0);
                if (cartao == null)
                {
                    return NotFound("Cartão não encontrado.");
                }
                Fatura fatura = await _faturaRepository.ObterFaturaPorCartaoMesAno(transacao.CartaoId ?? 0, transacao.Data.Month, transacao.Data.Year);
                if (fatura == null)
                {
                    return NotFound("Fatura não encontrada.");
                }
                transacao.FaturaId = fatura.Id;
                fatura.AdicionarTransacao(transacao);

                cartao.SubtrairSaldo(transacao.Valor);
                transacao.Pago = false;

                await _transacaoRepository.AddAsync(transacao);
                FaturaViewDTO faturaMapeada = _faturaMapping.MapToGetDTO(fatura);
                return Ok(faturaMapeada);
            }
            else if (transacao.MetodoPagamento != MetodoPagamento.CartaoCredito)
            {
                if (transacao.CartaoId.HasValue && transacao.CartaoId > 0)
                {
                    return BadRequest("CartaoId não deve ser passado nesse tipo de MetodoPagamento.");
                }
                Conta conta = await _contaRepository.GetEntityByIdAsync(transacao.ContaId);
                if (conta == null)
                {
                    return NotFound("Conta não encontrada.");
                }

                if (transacao.TipoTransacao == TipoTransacao.Receita)
                {
                    conta.AdicionarSaldo(transacao.Valor);
                }
                else if (transacao.TipoTransacao == TipoTransacao.Despesa)
                {
                    if (transacao.Valor > conta.Saldo)
                    {
                        return BadRequest("Saldo insuficiente na conta.");
                    }
                    conta.SubtrairSaldo(transacao.Valor);
                }

                //transacao.Pago = true; // Por padrão, transações de conta são marcadas como pagas

                await _transacaoRepository.AddAsync(transacao);

                return Ok("Transação adicionada com sucesso.");
            }
            return BadRequest("Operação cancelada!");
        }
        [HttpGet]
        public async Task<IActionResult> ListarTransacoes()
        {
            var objetosDb = await _transacaoRepository.ObterTransacoes();
            List<TransacaoViewDTO> objetosMapeados = new List<TransacaoViewDTO>();
            foreach (var objetoDb in objetosDb)
            {
                objetosMapeados.Add(_transacaoMapping.MapToGetDTO(objetoDb));
            }
            return Ok(objetosMapeados);
        }
    }
}
