﻿using Microsoft.AspNetCore.Identity;

using ProjControleFinanceiro.Data.Configuracao;
using ProjControleFinanceiro.Entities.Entidades;
using ProjControleFinanceiro.Entities.Entidades.Enums;
using ProjControleFinanceiro.Identity.Configuracao;

namespace ProjControleFinanceiro.Identity.Seeds.Configuracao;

public class CreateInitialAdminSeed
{
    private readonly ApplicationDbContext _contextIdentity;
    private readonly ContextoBase _contextBase;
    private readonly UserManager<IdentityUser> _userManager;

    public CreateInitialAdminSeed(ApplicationDbContext contextIdentity, ContextoBase contextBase, UserManager<IdentityUser> userManager)
    {
        _contextIdentity = contextIdentity;
        _contextBase = contextBase;
        _userManager = userManager;
    }

    public void Create()
    {
        Guid id = Guid.Parse("BBDC94BA-D192-409B-A9BF-BF5977B30425");
        var name = "Usuário";
        var lastName = "Administrador";
        var phoneNumber = "(99) 99999-9999";
        var email = "admin@controlefinanceiro.com";
        var password = "Admin@123";

        var user = new IdentityUser
        {
            Id = id.ToString(),
            UserName = email,
            Email = email,
            EmailConfirmed = true,
            PhoneNumber = phoneNumber,
            PhoneNumberConfirmed = true,
            NormalizedEmail = email.ToUpper(),
            NormalizedUserName = email.ToUpper()
        };
        

        var userExists = _userManager.FindByEmailAsync(user.Email).Result;

        if (userExists is null)
        {
            _userManager.CreateAsync(user, password).GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(user, RolesEnum.Admin.ToString()).GetAwaiter().GetResult();
            Cliente cliente = new Cliente()
            {
                Id = id,
                Name = name,
                LastName = lastName
            };
            _contextBase.Clientes.Add(cliente);
        }
        else
        {
            var roles = _userManager.GetRolesAsync(userExists).GetAwaiter().GetResult();
            if (roles == null || roles.Count == 0)
            {
                _userManager.AddToRoleAsync(userExists, RolesEnum.Admin.ToString()).GetAwaiter().GetResult();
            }
        }
        _contextBase.SaveChanges();
        _contextIdentity.SaveChanges();
    }
}