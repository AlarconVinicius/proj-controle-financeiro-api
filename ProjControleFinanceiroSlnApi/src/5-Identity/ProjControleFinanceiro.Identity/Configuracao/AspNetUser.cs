using System.Security.Claims;

using Microsoft.AspNetCore.Http;

using ProjControleFinanceiro.Domain.Interfaces.Services;

namespace ProjControleFinanceiro.Identity.Configuracao;
public class AspNetUser : IUser
{
    private readonly IHttpContextAccessor _accessor;

    public AspNetUser(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    public string Name => _accessor.HttpContext.User.Identity.Name;

    public bool IsAuthenticated => _accessor.HttpContext.User.Identity.IsAuthenticated;

    public string GetUserEmail()
    {
        if (!IsAuthenticated) return "";

        ClaimsPrincipal claims = _accessor.HttpContext.User;
        var email = claims.FindFirst(ClaimTypes.Name);

        return email?.Value;
    }

    public Guid GetUserId()
    {
        if (!IsAuthenticated) return Guid.Empty;

        ClaimsPrincipal claims = _accessor.HttpContext.User;
        var GuidId = claims.FindFirst(ClaimTypes.NameIdentifier);

        return Guid.Parse(GuidId?.Value);
    }


}
