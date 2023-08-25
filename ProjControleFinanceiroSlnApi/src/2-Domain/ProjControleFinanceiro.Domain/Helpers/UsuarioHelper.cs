using System.Security.Claims;

using Microsoft.AspNetCore.Http;

namespace ProjControleFinanceiro.Domain.Helpers;
public static class UsuarioHelper
{
    public static string GetName(IHttpContextAccessor accessor)
    {
        return accessor.HttpContext!.User.Identity!.Name!;
    }

    public static bool IsAuthenticated(IHttpContextAccessor accessor)
    {
        return accessor.HttpContext!.User.Identity!.IsAuthenticated;
    }

    public static string GetUserEmail(IHttpContextAccessor accessor)
    {
        if (!IsAuthenticated(accessor)) return "";

        ClaimsPrincipal claims = accessor.HttpContext!.User;
        var email = claims.FindFirst(ClaimTypes.Name);

        return email!.Value;
    }

    public static Guid GetUserId(IHttpContextAccessor accessor)
    {
        if (!IsAuthenticated(accessor)) return Guid.Empty;

        ClaimsPrincipal claims = accessor.HttpContext!.User;
        var GuidId = claims.FindFirst(ClaimTypes.NameIdentifier);

        return Guid.Parse(GuidId!.Value);
    }
}