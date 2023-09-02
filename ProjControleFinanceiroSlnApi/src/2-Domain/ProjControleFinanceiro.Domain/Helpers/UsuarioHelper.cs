using System.Security.Claims;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

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
    public static ClaimsPrincipal GetLoggedInUser(IHttpContextAccessor accessor)
    {
        if (!IsAuthenticated(accessor)) return null!;

        return accessor.HttpContext!.User;
    }

    public static bool IsAdmin(IHttpContextAccessor accessor, UserManager<IdentityUser> userManager)
    {
        var loggedInUser = GetLoggedInUser(accessor);

        if (loggedInUser == null)
        {
            return false;
        }

        var loggedInUserId = GetUserId(accessor);
        var userRoles = userManager.GetRolesAsync(userManager.FindByIdAsync(loggedInUserId.ToString()).Result).Result;

        return userRoles.Contains("Admin");
    }
}