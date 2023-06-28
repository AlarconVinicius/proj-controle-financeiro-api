using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ProjControleFinanceiro.Domain.DTOs.Conta;
using ProjControleFinanceiro.Domain.Exceptions;
using ProjControleFinanceiro.Domain.Extensions;
using ProjControleFinanceiro.Domain.Interfaces.Repositorios;
using ProjControleFinanceiro.Domain.Interfaces.Services;
using ProjControleFinanceiro.Domain.Services;
using ProjControleFinanceiro.Entities.Entidades;

namespace ProjControleFinanceiro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaController : MainController
    {
        private readonly IContaService _contaservice;


        public ContaController(IContaService contaService)
        {
            _contaservice = contaService;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarConta(ContaAddDTO objeto)
        {
            var objetoMapeadoView = await _contaservice.AdicionarConta(objeto);
            if(!_contaservice.OperacaoValida()) return CustomResponse(_contaservice.GetErrors());
            return CustomResponse(objetoMapeadoView);
        }

        [HttpGet]
        public async Task<IActionResult> ObterContas()
        {
            return CustomResponse(await _contaservice.ObterContas());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterContaPorId([FromRoute] int id)
        {
            var objetoMapeadoView = await _contaservice.ObterContaPorId(id);
            if (!_contaservice.OperacaoValida()) return CustomResponse(_contaservice.GetErrors());
            return CustomResponse(objetoMapeadoView);
        }

        [HttpPut]
        public async Task<IActionResult> AtualizarConta(ContaUpdDTO objeto)
        {
            var objetoMapeadoView = await _contaservice.AtualizarConta(objeto);
            if (!_contaservice.OperacaoValida()) return CustomResponse(_contaservice.GetErrors());
            return CustomResponse(objetoMapeadoView);
        }

        [HttpDelete]
        public async Task<IActionResult> DeletarConta([FromQuery] int id)
        {
            await _contaservice.DeletarConta(id);
            if (!_contaservice.OperacaoValida()) return CustomResponse(_contaservice.GetErrors());
            return CustomResponse();
        }
    }
}

