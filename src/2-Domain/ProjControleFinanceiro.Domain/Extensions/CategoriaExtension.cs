using ProjControleFinanceiro.Domain.DTOs.Enums;
using ProjControleFinanceiro.Entities.Entidades.Enums;

namespace ProjControleFinanceiro.Domain.Extensions
{
    public static class CategoriaExtension
    {
        public static CategoriaDTO ToGetDTO(this Categoria value)
        {
            return new CategoriaDTO
            {
                Id = value.GetHashCode(),
                Categoria = value.ToString()
            };
        }
    }
}
