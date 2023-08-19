using ProjControleFinanceiro.Domain.DTOs.Enums;
using ProjControleFinanceiro.Entities.Entidades.Enums;

namespace ProjControleFinanceiro.Domain.Extensions;

public static class CategoriaExtension
{
    public static CategoriaDto ToGetDTO(this Categoria value)
    {
        return new CategoriaDto
        {
            Id = value.GetHashCode(),
            Categoria = value.ToString()
        };
    }
}
