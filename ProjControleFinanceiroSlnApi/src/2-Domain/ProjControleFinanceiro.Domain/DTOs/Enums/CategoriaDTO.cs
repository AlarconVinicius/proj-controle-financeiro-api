using ProjControleFinanceiro.Entities.Entidades.Enums;

namespace ProjControleFinanceiro.Domain.DTOs.Enums
{
    public class CategoriaDTO
    {
        public int Id { get; set; }
        public string Categoria { get; set; }
        public CategoriaDTO()
        {
            Id = 0;
            Categoria = string.Empty;
        }
    }
}
