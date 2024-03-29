﻿using FluentValidation;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using ProjControleFinanceiro.Domain.DTOs.Usuario;
using ProjControleFinanceiro.Domain.Helpers;
using ProjControleFinanceiro.Domain.Interfaces.Repositorios;
using ProjControleFinanceiro.Domain.Services.Configuracao;
using ProjControleFinanceiro.Entities.Entidades;
using ProjControleFinanceiro.Identity.Domain.Interfaces;
using ProjControleFinanceiro.Identity.Extensions;

namespace ProjControleFinanceiro.Identity.Domain.Services;
public class UsuarioService : MainService, IUsuarioService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IValidator<UpdUserRequest> _updValidator;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IHttpContextAccessor _accessor;

    public UsuarioService(IUsuarioRepository usuarioRepository, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IValidator<UpdUserRequest> updValidator, IHttpContextAccessor accessor)
    {
        _userManager = userManager;
        _usuarioRepository = usuarioRepository;
        _updValidator = updValidator;
        _accessor = accessor;
    }

    public async Task<UserResponse> ObterUsuarioPorId(Guid id)
    {
        try
        {
            Guid idUsuarioLogado = UsuarioHelper.GetUserId(_accessor);
            if (!UsuarioHelper.IsAdmin(_accessor, _userManager) && id != idUsuarioLogado)
            {
                AdicionarErroProcessamento("Operação não permitida.");
                return null!;
            }
            Cliente usuarioDb = await _usuarioRepository.GetEntityByIdAsync(id);
            var usuarioIdentityDb = await _userManager.FindByIdAsync(id.ToString());

            if (usuarioIdentityDb is null && usuarioDb is null)
            {
                AdicionarErroProcessamento("Usuário não encontrado.");
                return null!;
            }
            var userRoles = (await _userManager.GetRolesAsync(usuarioIdentityDb!)).ToList();
            return usuarioDb.ToViewUsuarioDto(usuarioIdentityDb!, userRoles);
        }
        catch (Exception ex)
        {
            AdicionarErroProcessamento($"Erro ao obter usuário: {ex.Message}");
            return null!;
        }
    }

    public async Task<IEnumerable<UserResponse>> ObterUsuarios()
    {
        try
        {
            if (!UsuarioHelper.IsAdmin(_accessor, _userManager))
            {
                AdicionarErroProcessamento("Operação não permitida.");
                return null!;
            }
            List<Cliente> usuariosDb = await _usuarioRepository.ListAsync();
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
        catch (Exception ex)
        {
            AdicionarErroProcessamento($"Erro ao obter usuários: {ex.Message}");
            return null!;
        }
    }

    public async Task AtualizarUsuario(UpdUserRequest objeto)
    {
        try
        {
            var validationResult = await _updValidator.ValidateAsync(objeto);
            if (!validationResult.IsValid)
            {
                AdicionarErroProcessamento(validationResult);
                return;
            }
            Cliente usuarioDb = await _usuarioRepository.GetEntityByIdAsync(objeto.Id);
            var usuarioIdentityDb = await _userManager.FindByIdAsync(objeto.Id.ToString());
            if (usuarioIdentityDb is null && usuarioDb is null)
            {
                AdicionarErroProcessamento("Usuário não encontrado.");
                return;
            }
            Guid idUsuarioLogado = UsuarioHelper.GetUserId(_accessor);
            var usuarioLogado = await _userManager.FindByIdAsync(idUsuarioLogado.ToString());
            if (usuarioLogado is null)
            {
                AdicionarErroProcessamento("Usuário logado não encontrado.");
                return;
            }
            var userRoles = (await _userManager.GetRolesAsync(usuarioLogado)).ToList();
            if (!userRoles.Contains(Roles.Admin) && objeto.Id != idUsuarioLogado)
            {
                AdicionarErroProcessamento("Operação não permitida.");
                return;
            }
            usuarioDb.Name = objeto.Name;
            usuarioDb.LastName = objeto.LastName;
            usuarioIdentityDb!.UserName = objeto.Email;
            usuarioIdentityDb.Email = objeto.Email;
            usuarioIdentityDb.PhoneNumber = objeto.PhoneNumber;
            usuarioIdentityDb.NormalizedEmail = objeto.Email.ToUpper();
            usuarioIdentityDb.NormalizedUserName = objeto.Email.ToUpper();

            await _usuarioRepository.UpdateAsync(usuarioDb);
            await _userManager.UpdateAsync(usuarioIdentityDb);
        }
        catch (Exception ex)
        {
            AdicionarErroProcessamento($"Erro ao atualizar usuário: {ex.Message}");
        }
    }
    public async Task AlterarStatusBloqueioUsuario(Guid userId, bool bloquear)
    {
        try
        {  
            DateTimeOffset lockoutEndDate = DateTime.Now;
            if (userId == UsuarioHelper.GetUserId(_accessor) || !UsuarioHelper.IsAdmin(_accessor, _userManager))
            {
                AdicionarErroProcessamento("Operação não permitida");
                return;
            }
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
            {
                AdicionarErroProcessamento("Usuário não encontrado.");
                return;
            }
            if (bloquear)
            {
                lockoutEndDate = lockoutEndDate.AddYears(1000);
            }
            await _userManager.SetLockoutEndDateAsync(user, lockoutEndDate);
        }
        catch (Exception ex)
        {
            AdicionarErroProcessamento($"Erro ao bloquear usuário: {ex.Message}");
        }
    }

    public async Task DeletarUsuario(Guid userId)
    {
        try
        {
            if (userId != UsuarioHelper.GetUserId(_accessor) && !UsuarioHelper.IsAdmin(_accessor, _userManager))
            {
                AdicionarErroProcessamento("Operação não permitida");
                return;
            }
            Cliente usuarioDb = await _usuarioRepository.GetEntityByIdAsync(userId);
            var usuarioIdentityDb = await _userManager.FindByIdAsync(userId.ToString());
            if (usuarioIdentityDb is null && usuarioDb is null)
            {
                AdicionarErroProcessamento("Usuário não encontrado.");
                return;
            }
            await _usuarioRepository.DeleteAsync(userId);
            await _userManager.DeleteAsync(usuarioIdentityDb!);
        }
        catch (Exception ex)
        {
            AdicionarErroProcessamento($"Erro ao deletar usuário: {ex.Message}");
        }
    }
}
