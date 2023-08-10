using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjControleFinanceiro.Api.Controllers.Configuracao;
using ProjControleFinanceiro.Domain.DTOs.Transacao.Relatorio;
using ProjControleFinanceiro.Domain.Interfaces.Services;

namespace ProjControleFinanceiro.Api.Controllers
{
    [Route("api/relatorios")]
    [Authorize]
    public class RelatorioController : MainController
    {
        private readonly IRelatorioService _relatorioService;

        public RelatorioController(IRelatorioService relatorioService)
        {
            _relatorioService = relatorioService;
        }

        /// <summary>
        /// Gerar PDF com base no filtro de transações
        /// </summary>
        /// <returns>Resposta de sucesso.</returns>
        /// <response code="200">Indica que a geração do relatório obteve sucesso</response>
        /// <response code="400">Retorna erros de validação ou problemas na requisição.</response>
        [ProducesResponseType(typeof(ApiSuccessResponse<object>), 200)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> GerarPdf([FromQuery] RelatorioPDF query)
        {
            Byte[] pdfEmByte = await _relatorioService.GerarRelatorio(query);
            if (!_relatorioService.OperacaoValida()) return CustomResponse(_relatorioService.GetErrors());
            return CustomResponse(File(pdfEmByte, "application/pdf", "relatorio.pdf"));
        }
    }
}
