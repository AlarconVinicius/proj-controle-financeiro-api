using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using ProjControleFinanceiro.Domain.DTOs.Usuario;
using ProjControleFinanceiro.Domain.Interfaces.Repositorios;
using ProjControleFinanceiro.Domain.Services.Configuracao;
using ProjControleFinanceiro.Entities.Entidades;
using ProjControleFinanceiro.Identity.Domain.Interfaces;
using ProjControleFinanceiro.Identity.Extensions;

namespace ProjControleFinanceiro.Identity.Domain.Services;
public class UsuarioService : MainService, IUsuarioService
{
    private readonly IUsuarioRepository _clienteRepository;
    private readonly UserManager<IdentityUser> _userManager;

    public UsuarioService(IUsuarioRepository clienteRepository, UserManager<IdentityUser> userManager)
    {
        _clienteRepository = clienteRepository;
        _userManager = userManager;
    }

    public async Task<UserResponse> ObterUsuarioPorId(Guid id)
    {
        Cliente usuarioDb = await _clienteRepository.GetEntityByIdAsync(id);
        var usuarioIdentityDb = await _userManager.FindByIdAsync(id.ToString());

        if (usuarioIdentityDb is null && usuarioDb is null) 
        {
            AdicionarErroProcessamento("Usuário não encontrado.");
            return null!;
        }
        var userRoles = (await _userManager.GetRolesAsync(usuarioIdentityDb!)).ToList();
        return usuarioDb.ToViewUsuarioDto(usuarioIdentityDb!, userRoles);
    }

    public async Task<IEnumerable<UserResponse>> ObterUsuarios()
    {
        List<Cliente> usuariosDb = await _clienteRepository.ListAsync();
        List<IdentityUser> usuariosIdentityDb = await _userManager.Users.ToListAsync();

        var usuariosResponses = new List<UserResponse>();

        foreach (var usuarioDb in usuariosDb)
        {
            var usuarioIdentityDb = usuariosIdentityDb.First(u => u.Id == usuarioDb.Id.ToString());

            var userRoles = (await _userManager.GetRolesAsync(usuarioIdentityDb)).ToList();

            usuariosResponses.Add(usuarioDb.ToViewUsuarioDto(usuarioIdentityDb, userRoles));
        }

        return usuariosResponses;
    }
}
