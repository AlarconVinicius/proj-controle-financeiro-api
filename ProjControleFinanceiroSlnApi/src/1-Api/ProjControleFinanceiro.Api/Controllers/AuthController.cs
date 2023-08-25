using Microsoft.AspNetCore.Mvc;

using ProjControleFinanceiro.Api.Controllers.Configuracao;
using ProjControleFinanceiro.Domain.DTOs.Usuario;
using ProjControleFinanceiro.Identity.Domain.Interfaces;

namespace ProjControleFinanceiro.Api.Controllers;

[Route("api")]
public class AuthController : MainController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Adiciona um novo usuário.
    /// </summary>
    /// <param name="registroUsuario">Objeto contendo os dados do usuário a ser adicionado.</param>
    /// <returns>O usuário adicionado.</returns>
    /// <response code="200">Retorna o usuário adicionado com sucesso.</response>
    /// <response code="400">Retorna erros de validação ou problemas na requisição.</response>
    [ProducesResponseType(typeof(ApiSuccessResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [HttpPost("registrar")]
    public async Task<ActionResult> Registar(UsuarioViewModel registroUsuario)
    {
        if(!ModelState.IsValid) { return CustomResponse(ModelState); }
        await _authService.RegistrarUsuario(registroUsuario);
        return _authService.OperacaoValida() ? CustomResponse() : CustomResponse(_authService.GetErrors());
    }

    /// <summary>
    /// Autentica um usuário.
    /// </summary>
    /// <param name="usuarioLogin">Objeto contendo os dados do usuário a ser autenticado.</param>
    /// <returns>O usuário autenticado.</returns>
    /// <response code="200">Retorna o usuário autenticado com sucesso.</response>
    /// <response code="400">Retorna erros de validação ou problemas na requisição.</response>
    [ProducesResponseType(typeof(ApiSuccessResponse<UsuarioRespostaLogin>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [HttpPost("autenticar")]
    public async Task<ActionResult> Login(LoginUserViewModel usuarioLogin)
    {
        if (!ModelState.IsValid) { return CustomResponse(ModelState); }
        var result = await _authService.LogarUsuario(usuarioLogin);
        return _authService.OperacaoValida() ? CustomResponse(result) : CustomResponse(_authService.GetErrors());
    }
}
