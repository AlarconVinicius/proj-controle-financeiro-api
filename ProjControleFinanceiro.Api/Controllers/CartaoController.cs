using Microsoft.AspNetCore.Mvc;
using ProjControleFinanceiro.Domain.DTOs.Cartao;
using ProjControleFinanceiro.Domain.Interfaces.Services;

namespace ProjControleFinanceiro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartaoController : MainController
    {
        private readonly ICartaoService _cartaoService;
        public CartaoController(ICartaoService cartaoService)
        {
            _cartaoService = cartaoService;
        }
        [HttpPost]
        public async Task<IActionResult> AdicionarCartao(CartaoAddDTO CartaoAddDTO)
        {
            var objetoMapeadoView = await _cartaoService.AdicionarCartao(CartaoAddDTO);
            if(!_cartaoService.OperacaoValida()) return CustomResponse(_cartaoService.GetErrors());
            return CustomResponse(objetoMapeadoView);
        }

        [HttpGet]
        public async Task<IActionResult> ObterCartoes()
        {
            return CustomResponse(await _cartaoService.ObterCartoes());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterCartaoPorId([FromRoute] int id)
        {
            var objetoMapeadoView = await _cartaoService.ObterCartaoPorId(id);
            if (!_cartaoService.OperacaoValida()) return CustomResponse(_cartaoService.GetErrors());
            return CustomResponse(objetoMapeadoView);
        }

        [HttpPut]
        public async Task<IActionResult> AtualizarCartao(CartaoUpdDTO objeto)
        {
            var objetoMapeadoView = await _cartaoService.AtualizarCartao(objeto);
            if (!_cartaoService.OperacaoValida()) return CustomResponse(_cartaoService.GetErrors());
            return CustomResponse(objetoMapeadoView);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarCartao([FromRoute] int id)
        {
            await _cartaoService.DeletarCartao(id);
            if (!_cartaoService.OperacaoValida()) return CustomResponse(_cartaoService.GetErrors());
            return CustomResponse();
        }
    }
}
