using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjControleFinanceiro.Api.Controllers.Configuracao;
using ProjControleFinanceiro.Domain.DTOs.Transacao;
using ProjControleFinanceiro.Domain.Interfaces.Services;

namespace ProjControleFinanceiro.Api.Controllers
{

    [Route("api/transacoes")]
    [Authorize]
    public class TransacaoController : MainController
    {
        private readonly ITransacaoService _transacaoService;
        public TransacaoController(ITransacaoService transacaoService)
        {
            _transacaoService = transacaoService;
        }

        /// <summary>
        /// Adiciona uma nova transação.
        /// </summary>
        /// <param name="objeto">Objeto contendo os dados da transação a ser adicionada.</param>
        /// <returns>A transação adicionada.</returns>
        /// <response code="200">Retorna a transação adicionada com sucesso.</response>
        /// <response code="400">Retorna erros de validação ou problemas na requisição.</response>
        [ProducesResponseType(typeof(ApiSuccessResponse<TransacaoViewDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> AdicionarTransacao(TransacaoAddDTO objeto)
        {
            var objetoMapeado = await _transacaoService.AdicionarTransacao(objeto);
            if (!_transacaoService.OperacaoValida()) return CustomResponse(_transacaoService.GetErrors());
            return CustomResponse(objetoMapeado);
        }

        /// <summary>
        /// Obtém uma transação pelo seu ID.
        /// </summary>
        /// <param name="id">ID da transação a ser obtida.</param>
        /// <returns>A transação correspondente ao ID informado.</returns>
        /// <response code="200">Retorna a transação obtida com sucesso.</response>
        /// <response code="400">Retorna erros de validação ou problemas na requisição.</response>
        [ProducesResponseType(typeof(ApiSuccessResponse<TransacaoViewDTO>), 200)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public async Task<IActionResult> ObterTransacaoPorId([FromRoute] Guid id)
        {
            var objetoMapeado = await _transacaoService.ObterTransacaoPorId(id);
            if (!_transacaoService.OperacaoValida()) return CustomResponse(_transacaoService.GetErrors());
            return CustomResponse(objetoMapeado);
        }

        /// <summary>
        /// Obtém todas as transações.
        /// </summary>
        /// <returns>Lista de todas as transações.</returns>
        /// <response code="200">Retorna a lista de transações obtidas com sucesso.</response>
        /// <response code="400">Retorna erros de validação ou problemas na requisição.</response> 
        [ProducesResponseType(typeof(ApiSuccessResponse<TransacaoViewListDTO>), 200)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<IActionResult> ObterTransacoes()
        {            
            var objetosMapeados = CustomResponse(await _transacaoService.ObterTransacoes());
            if (!_transacaoService.OperacaoValida()) return CustomResponse(_transacaoService.GetErrors());
            return CustomResponse(objetosMapeados);
        }

        /// <summary>
        /// Obtém todas as transações de um respectivo mês e ano.
        /// </summary>
        /// <returns>Lista de todas as transações do mês e ano enviados.</returns>
        /// <response code="200">Retorna a lista de transações obtidas com sucesso.</response>
        /// <response code="400">Retorna erros de validação ou problemas na requisição.</response>
        [ProducesResponseType(typeof(ApiSuccessResponse<TransacaoViewListDTO>), 200)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpGet("mes-ano")]
        public async Task<IActionResult> ObterTransacoesMesAno([FromQuery] int mes, [FromQuery] int ano)
        {
            var objetosMapeados = CustomResponse(await _transacaoService.ObterTransacoesMesAno(mes, ano));
            if (!_transacaoService.OperacaoValida()) return CustomResponse(_transacaoService.GetErrors());
            return CustomResponse(objetosMapeados);
        }

        /// <summary>
        /// Atualiza uma transação.
        /// </summary>
        /// <param name="objeto">Objeto contendo os dados da transação a ser atualizada.</param>
        /// <returns>Resposta de sucesso.</returns>
        /// <response code="200">Indica que a transação foi atualizada com sucesso.</response>
        /// <response code="400">Retorna erros de validação ou problemas na requisição.</response>
        [ProducesResponseType(typeof(ApiSuccessResponse<object>), 200)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpPut]
        public async Task<IActionResult> AtualizarTransacao(TransacaoUpdDTO objeto)
        {
            await _transacaoService.AtualizarTransacao(objeto);
            if (!_transacaoService.OperacaoValida()) return CustomResponse(_transacaoService.GetErrors());
            return CustomResponse();
        }

        /// <summary>
        /// Atualiza o status de pagamento de uma transação.
        /// </summary>
        /// <param name="id">ID da transação a ser atualizada.</param>
        /// <param name="pago">Valor booleano indicando se a transação foi paga ou não.</param>
        /// <returns>Resposta de sucesso.</returns>
        /// <response code="200">Indica que o status de pagamento da transação foi atualizado com sucesso.</response>
        /// <response code="400">Retorna erros de validação ou problemas na requisição.</response>
        [ProducesResponseType(typeof(ApiSuccessResponse<object>), 200)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpPatch("{id}/pago")]
        public async Task<IActionResult> AtualizarStatusPagamento([FromRoute] Guid id, [FromBody] bool pago)
        {
            await _transacaoService.AtualizarStatusPagamento(id, pago);
            if (!_transacaoService.OperacaoValida()) return CustomResponse(_transacaoService.GetErrors());
            return CustomResponse();
        }

        /// <summary>
        /// Deleta uma transação.
        /// </summary>
        /// <param name="id">ID da transação a ser deletada.</param>
        /// <returns>Resposta de sucesso.</returns>
        /// <response code="200">Indica que a transação foi deletada com sucesso.</response>
        /// <response code="400">Retorna erros de validação ou problemas na requisição.</response>
        [ProducesResponseType(typeof(ApiSuccessResponse<object>), 200)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarTransacao([FromRoute] Guid id)
        {
            await _transacaoService.DeletarTransacao(id);
            if (!_transacaoService.OperacaoValida()) return CustomResponse(_transacaoService.GetErrors());
            return CustomResponse();
        }
    }
}
