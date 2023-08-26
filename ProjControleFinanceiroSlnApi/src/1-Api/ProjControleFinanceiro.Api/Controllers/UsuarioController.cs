using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ProjControleFinanceiro.Api.Controllers.Configuracao;
using ProjControleFinanceiro.Domain.DTOs.Usuario;
using ProjControleFinanceiro.Domain.Helpers;
using ProjControleFinanceiro.Identity.Domain.Interfaces;

namespace ProjControleFinanceiro.Api.Controllers;

[Route("api/usuarios")]
[Authorize]
public class UsuarioController : MainController
{
    private readonly IHttpContextAccessor _accessor;
    private readonly IUsuarioService _usuarioService;
    public UsuarioController(IHttpContextAccessor accessor, IUsuarioService usuarioService)
    {
        _accessor = accessor;
        _usuarioService = usuarioService;
    }

    [HttpGet("{id}")]
    public async Task<UserResponse> ObterUsuarioPorId(Guid id)
    {
        return await _usuarioService.ObterUsuarioPorId(id);
    }
    [HttpGet]
    public async Task<IEnumerable<UserResponse>> ObterUsuarios()
    {
        return await _usuarioService.ObterUsuarios();
    }

    [HttpPut]
    public async Task<IActionResult> AtualizarUsuario(UpdUserRequest objeto)
    {

        Guid idUsuarioLogado = UsuarioHelper.GetUserId(_accessor);
        await _usuarioService.AtualizarUsuario(idUsuarioLogado, objeto);
        if (!_usuarioService.OperacaoValida()) return CustomResponse(_usuarioService.GetErrors());
        return CustomResponse();
    }
}
