﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using FluentValidation;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using ProjControleFinanceiro.Domain.DTOs.Usuario;
using ProjControleFinanceiro.Domain.Interfaces.Repositorios;
using ProjControleFinanceiro.Domain.Services.Configuracao;
using ProjControleFinanceiro.Entities.Entidades;
using ProjControleFinanceiro.Entities.Entidades.Enums;
using ProjControleFinanceiro.Identity.Configuracao;
using ProjControleFinanceiro.Identity.Domain.Interfaces;
using ProjControleFinanceiro.Identity.Extensions;

namespace ProjControleFinanceiro.Identity.Domain.Services;
public class AuthService : MainService, IAuthService
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IUsuarioRepository _cliente;
    private readonly AppSettings _appSettings;
    private readonly IValidator<AddUserRequest> _addValidator;
    private readonly IValidator<LoginUserRequest> _loginValidator;

    public AuthService(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IUsuarioRepository cliente, IOptions<AppSettings> appSettings, IValidator<AddUserRequest> addValidator, IValidator<LoginUserRequest> loginValidator)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _appSettings = appSettings.Value;
        _cliente = cliente;
        _addValidator = addValidator;
        _loginValidator = loginValidator;
    }
    public async Task RegistrarUsuario(AddUserRequest objeto)
    {
        try
        {
            var validationResult = await _addValidator.ValidateAsync(objeto);
            if (!validationResult.IsValid)
            {
                AdicionarErroProcessamento(validationResult);
                return;
            }

            var user = objeto.ToAddClienteIdentity();

            var result = await _userManager.CreateAsync(user, objeto.Password);
            if (result.Succeeded)
            {
                var userIdentity = await _userManager.FindByEmailAsync(objeto.Email);
                await _userManager.AddToRoleAsync(userIdentity!, RolesEnum.User.ToString());
                Guid userId = Guid.Parse(userIdentity!.Id);

                Cliente cliente = objeto.ToAddClienteDto(userId);

                await _cliente.AddAsync(cliente);

                await _signInManager.SignInAsync(user, false);

                return;

            }

            foreach (var erros in result.Errors)
            {
                AdicionarErroProcessamento(erros.Description);
            }

            return;
        }
        catch (Exception ex)
        {
            AdicionarErroProcessamento($"Erro ao registrar usuário: {ex.Message}");
            return;
        }
    }

    public async Task<UsuarioRespostaLogin> LogarUsuario(LoginUserRequest objeto)
    {
        try
        {
            var validationResult = await _loginValidator.ValidateAsync(objeto);
            if (!validationResult.IsValid)
            {
                AdicionarErroProcessamento(validationResult);
                return null!;
            }
            var result = await _signInManager.PasswordSignInAsync(objeto.Email, objeto.Password, false, true);

            if (result.Succeeded)
            {
                return await GerarJwt(objeto.Email);
            }
            else if (result.IsLockedOut)
            {
                var user = await _userManager.FindByEmailAsync(objeto.Email);
                var lockoutTime = await _userManager.GetLockoutEndDateAsync(user!);
                var dataDesbloqueio = lockoutTime?.ToString("dd/MM/yyyy HH:mm");
                string mensagem = $"O bloqueio expira em: {dataDesbloqueio}";
                AdicionarErroProcessamento($"Usuário bloqueado. {mensagem}");
                return null!;
            }
            AdicionarErroProcessamento("Usuário ou senha inválido!");
            return null!;
        }
        catch(Exception ex)
        {
            AdicionarErroProcessamento($"Erro ao logar o usuário: {ex.Message}");
            return null!;
        }
    }

    private async Task<UsuarioRespostaLogin> GerarJwt(string email)
    {
        var userDb = await _userManager.FindByEmailAsync(email);
        if (userDb == null)
        {
            AdicionarErroProcessamento("Usuário não encontrado!");
            return null!;
        }
        var claims = (await _userManager.GetClaimsAsync(userDb)).ToList();
        var userRoles = await _userManager.GetRolesAsync(userDb);

        AddStandardClaims(claims, userDb);
        AddUserRolesClaims(claims, userRoles);

        var token = GenerateToken(claims);
        var response = CreateResponse(token, userDb, claims);

        return response;
    }
    private void AddStandardClaims(List<Claim> claims, IdentityUser user)
    {
        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email!));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));
    }
    private void AddUserRolesClaims(List<Claim> claims, IList<string> userRoles)
    {
        foreach (var userRole in userRoles)
        {
            claims.Add(new Claim("role", userRole));
        }
    }

    private SecurityToken GenerateToken(List<Claim> claims)
    {
        var identityClaims = new ClaimsIdentity();
        identityClaims.AddClaims(claims);

        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

        var tokenHandler = new JwtSecurityTokenHandler();

        return tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _appSettings.Emissor,
            Audience = _appSettings.ValidoEm,
            Subject = identityClaims,
            Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        });
    }
    private UsuarioRespostaLogin CreateResponse(SecurityToken token, IdentityUser user, List<Claim> claims)
    {
        var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);

        return new UsuarioRespostaLogin
        {
            AccessToken = encodedToken,
            ExpiresIn = TimeSpan.FromHours(_appSettings.ExpiracaoHoras).TotalSeconds,
            UsuarioToken = new UsuarioToken
            {
                Id = user.Id,
                Email = user.Email!,
                Claims = claims.Select(c => new UsuarioClaim { Type = c.Type, Value = c.Value })
            }
        };
    }
    private static long ToUnixEpochDate(DateTime date)
        => (long)Math.Round((date.ToUniversalTime() - DateTimeOffset.UnixEpoch).TotalSeconds);
}