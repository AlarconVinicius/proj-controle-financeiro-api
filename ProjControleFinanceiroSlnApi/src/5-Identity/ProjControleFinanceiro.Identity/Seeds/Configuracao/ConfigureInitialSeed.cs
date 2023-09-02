using Microsoft.AspNetCore.Identity;

using ProjControleFinanceiro.Data.Configuracao;
using ProjControleFinanceiro.Data.Seeds.Configuracao;
using ProjControleFinanceiro.Identity.Configuracao;

namespace ProjControleFinanceiro.Identity.Seeds.Configuracao;
public class ConfigureInitialSeed
{

    private readonly ApplicationDbContext _contextIdentity;
    private readonly ContextoBase _contextBase;
    private readonly UserManager<IdentityUser> _userManager;

    public ConfigureInitialSeed(ApplicationDbContext contextIdentity, ContextoBase contextBase, UserManager<IdentityUser> userManager)
    {
        _contextIdentity = contextIdentity;
        _contextBase = contextBase;
        _userManager = userManager;
    }

    public void StartConfig()
    {
        new CreateInitialRolesSeed(_contextIdentity).Create();
        new CreateInitialAdminSeed(_contextIdentity, _contextBase, _userManager!).Create();
        new CreateInitialTransactions(_contextBase).Create();
    }
}
