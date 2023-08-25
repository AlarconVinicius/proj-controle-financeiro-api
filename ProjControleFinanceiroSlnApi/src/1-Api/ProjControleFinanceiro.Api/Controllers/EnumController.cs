using Microsoft.AspNetCore.Mvc;

using ProjControleFinanceiro.Api.Controllers.Configuracao;
using ProjControleFinanceiro.Domain.Interfaces.Services;

namespace ProjControleFinanceiro.Api.Controllers;

[Route("api/enums")]
[Authorize]
public class EnumController : MainController
{
    private readonly IEnumService _enumService;

    public EnumController(IEnumService enumService)
    {
        _enumService = enumService;
    }

    [HttpGet("categorias")]
    public IActionResult ObterCategoriasEnum()
    {
        return CustomResponse(_enumService.ObterCategoriasEnum());
    }

    [HttpGet("tipos-transacao")]
    public IActionResult ObterTiposTransacaoEnum()
    {
        return CustomResponse(_enumService.ObterTiposTransacaoEnum());
    }
}
