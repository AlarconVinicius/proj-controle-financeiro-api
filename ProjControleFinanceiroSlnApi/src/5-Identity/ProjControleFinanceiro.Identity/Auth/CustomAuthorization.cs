using Microsoft.AspNetCore.Http;

namespace ProjControleFinanceiro.Identity.Auth;

public static class CustomAuthorization
{
    public static bool ValidarClaimsUsuario(HttpContext context, string claimName, string claimValue)
    {
        return context.User.Identity!.IsAuthenticated && context.User.Claims.Any(c => c.Type == claimName && c.Value.Contains(claimValue));
    }
}