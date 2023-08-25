using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

using ProjControleFinanceiro.Domain.DTOs.Usuario;
using ProjControleFinanceiro.Entities.Entidades;

namespace ProjControleFinanceiro.Domain.Extensions;
public static class UserExtension
{
    public static IdentityUser ToAddClienteIdentity(this AddUserRequest value)
    {
        return new IdentityUser
        {
            UserName = value.Email,
            Email = value.Email,
            EmailConfirmed = true,
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
            LastName = value.LastName,
            PhoneNumber = value.PhoneNumber
        };
    }
}
