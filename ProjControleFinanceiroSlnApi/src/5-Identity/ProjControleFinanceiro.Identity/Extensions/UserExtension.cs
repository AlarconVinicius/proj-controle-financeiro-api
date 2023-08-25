using Microsoft.AspNetCore.Identity;

using ProjControleFinanceiro.Domain.DTOs.Usuario;
using ProjControleFinanceiro.Entities.Entidades;

namespace ProjControleFinanceiro.Identity.Extensions;
public static class UserExtension
{
    public static IdentityUser ToAddClienteIdentity(this AddUserRequest value)
    {
        return new IdentityUser
        {
            UserName = value.Email,
            Email = value.Email,
            EmailConfirmed = true,
            PhoneNumber = value.PhoneNumber,
            PhoneNumberConfirmed = true,
            NormalizedEmail = value.Email.ToUpper(),
            NormalizedUserName = value.Email.ToUpper()
        };
    }
    public static Cliente ToAddClienteDto(this AddUserRequest value, Guid id)
    {
        return new Cliente()
        {
            Id = id,
            Name = value.Name,
            LastName = value.LastName
        };
    }
    public static UserResponse ToViewUsuarioDto(this Cliente value, IdentityUser valueIdentity, List<string> roles)
    {
        return new UserResponse(value.Id, value.Name, value.LastName, valueIdentity.Email!, valueIdentity.PhoneNumber!, roles);
    }
}
