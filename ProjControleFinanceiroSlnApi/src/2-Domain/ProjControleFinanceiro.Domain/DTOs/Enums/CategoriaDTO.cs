namespace ProjControleFinanceiro.Domain.DTOs.Enums;

public class CategoriaDto
{
    public int Id { get; set; }
    public string Categoria { get; set; }
    public CategoriaDto()
    {
        Id = 0;
        Categoria = string.Empty;
    }
}
