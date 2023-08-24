using Microsoft.AspNetCore.Identity;

using ProjControleFinanceiro.Data.Configuracao;
using ProjControleFinanceiro.Entities.Entidades;
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
        var email = "admin@controlefinanceiro.com";
        var password = "Admin@123";
        var user = new IdentityUser
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true,
            NormalizedEmail = email.ToUpper(),
            NormalizedUserName = email.ToUpper()
        };
        

        var userExists = _userManager.FindByEmailAsync(user.Email).Result;

        if (userExists is null)
        {
            _userManager.CreateAsync(user, password).GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(user, RolesEnum.Admin.ToString()).GetAwaiter().GetResult();
            var userCreated = _userManager.FindByEmailAsync(user.Email).Result;
            Cliente cliente = new Cliente()
            {
                Id = Guid.Parse(userCreated!.Id)
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