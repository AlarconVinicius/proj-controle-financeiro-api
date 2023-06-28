using Microsoft.AspNetCore.Mvc;
using ProjControleFinanceiro.Domain.DTOs.Transacao;
using ProjControleFinanceiro.Domain.Interfaces.Services;

namespace ProjControleFinanceiro.Api.Controllers
{
    [ApiController]
    [Route("api/transacoes")]
    public class TransacaoController : MainController
    {
        private readonly ITransacaoService _transacaoService;
        public TransacaoController(ITransacaoService transacaoService)
        {
            _transacaoService = transacaoService;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarTransacao(TransacaoAddDTO objeto)
        {
            var objetoMapeado = await _transacaoService.AdicionarTransacao(objeto);
            if (!_transacaoService.OperacaoValida()) return CustomResponse(_transacaoService.GetErrors());
            return CustomResponse(objetoMapeado);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> ObterTransacaoPorId([FromRoute] int id)
        {
            var objetoMapeado = await _transacaoService.ObterTransacaoPorId(id);
            if (!_transacaoService.OperacaoValida()) return CustomResponse(_transacaoService.GetErrors());
            return CustomResponse(objetoMapeado);
        }
        [HttpGet]
        public async Task<IActionResult> ObterTransacoes()
        {            
            return CustomResponse(await _transacaoService.ObterTransacoes());
        }
    }
}
