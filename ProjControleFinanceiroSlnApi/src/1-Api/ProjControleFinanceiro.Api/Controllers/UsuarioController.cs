using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ProjControleFinanceiro.Api.Controllers.Configuracao;
using ProjControleFinanceiro.Domain.DTOs.Usuario;
using ProjControleFinanceiro.Identity.Domain.Interfaces;

namespace ProjControleFinanceiro.Api.Controllers;

[Route("api/usuarios")]
[Authorize]
public class UsuarioController : MainController
{
    private readonly IUsuarioService _usuarioService;
    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterUsuarioPorId(Guid id)
    {
        var objetoMapeados = await _usuarioService.ObterUsuarioPorId(id);
        if (!_usuarioService.OperacaoValida()) return CustomResponse(_usuarioService.GetErrors());
        return CustomResponse(objetoMapeados);
    }

    [HttpGet]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> ObterUsuarios()
    {
        var objetosMapeados = await _usuarioService.ObterUsuarios();
        if (!_usuarioService.OperacaoValida()) return CustomResponse(_usuarioService.GetErrors());
        return CustomResponse(objetosMapeados);
    }

    [HttpPut]
    public async Task<IActionResult> AtualizarUsuario(UpdUserRequest objeto)
    {
        await _usuarioService.AtualizarUsuario(objeto);
        if (!_usuarioService.OperacaoValida()) return CustomResponse(_usuarioService.GetErrors());
        return CustomResponse();
    }

    [HttpPatch("{userId}/bloqueio")]
    public async Task<IActionResult> AlterarStatusBloqueioUsuario(Guid userId, [FromBody] bool bloquear)
    {
        await _usuarioService.AlterarStatusBloqueioUsuario(userId, bloquear);
        if (!_usuarioService.OperacaoValida()) return CustomResponse(_usuarioService.GetErrors());
        return CustomResponse();
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeletarUsuario(Guid userId)
    {
        await _usuarioService.DeletarUsuario(userId);
        if (!_usuarioService.OperacaoValida()) return CustomResponse(_usuarioService.GetErrors());
        return CustomResponse();
    }
}
