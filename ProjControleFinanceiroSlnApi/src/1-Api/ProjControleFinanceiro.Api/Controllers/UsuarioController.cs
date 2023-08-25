using Microsoft.AspNetCore.Mvc;

using ProjControleFinanceiro.Api.Controllers.Configuracao;
using ProjControleFinanceiro.Domain.DTOs.Usuario;
using ProjControleFinanceiro.Identity.Domain.Interfaces;

namespace ProjControleFinanceiro.Api.Controllers;

[Route("api/usuarios")]
public class UsuarioController : MainController
{
    private readonly IUsuarioService _usuarioService;
    public UsuarioController(IUsuarioService usuarioService)
    {
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
}
