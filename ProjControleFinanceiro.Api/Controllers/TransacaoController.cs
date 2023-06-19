using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ProjControleFinanceiro.Domain.DTOs.Transacao;
using ProjControleFinanceiro.Domain.Exceptions;
using ProjControleFinanceiro.Domain.Extensions;
using ProjControleFinanceiro.Domain.Interfaces.Services;
using ProjControleFinanceiro.Entities.Entidades;
using ProjControleFinanceiro.Entities.Filtros;

namespace ProjControleFinanceiro.Api.Controllers
{
    [ApiController]
    [Route("api/transacoes")]
    public class TransacaoController : MainController
    {
        private readonly ITransacaoService _transacaoService;
        private readonly IValidator<TransacaoAddDTO> _addValidator;
        public TransacaoController(IValidator<TransacaoAddDTO> addValidator, ITransacaoService transacaoService)
        {
            _addValidator = addValidator;
            _transacaoService = transacaoService;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarTransacao(TransacaoAddDTO objeto)
        {
            Transacao objetoMapeado;
            var validationResult = await _addValidator.ValidateAsync(objeto);
            if (!validationResult.IsValid) return CustomResponse(validationResult);
            if (objeto.CartaoId > 0 || objeto.CartaoId != 0)
            {
                objetoMapeado = objeto.ToAddDTO();
            }
            else
            {
                objetoMapeado = objeto.ToAddDTO2();
            }
            try
            {
                await _transacaoService.AdicionarTransacao(objetoMapeado);
            }
            catch (ServiceException ex)
            {
                AdicionarErroProcessamento(ex.Message);
                return CustomResponse();
            }
            return CustomResponse(objeto);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> ObterTransacaoPorId([FromRoute] int id)
        {
            try
            {
                var objetoDb = await _transacaoService.ObterTransacaoPorId(id);
                var objetoMapeado = objetoDb.ToGetDTO();
                return CustomResponse(objetoMapeado);
            }
            catch (ServiceException ex)
            {
                AdicionarErroProcessamento(ex.Message);
                return CustomResponse();
            }
        }
        [HttpGet]
        public async Task<IActionResult> ObterTransacoes([FromQuery] TransacaoFiltro filtro)
        {
            var objetosDb = await _transacaoService.ObterTransacoes(filtro);
            IEnumerable<TransacaoViewDTO> objetosMapeados = objetosDb.Select(x => x.ToGetDTO());
            return CustomResponse(objetosMapeados);
        }
    }
}
