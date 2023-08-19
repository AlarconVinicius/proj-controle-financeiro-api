namespace ProjControleFinanceiro.Domain.DTOs.Enums;

public class TipoTransacaoDto
{
    public int Id { get; set; }
    public string TipoTransacao { get; set; }
    public TipoTransacaoDto()
    {
        Id = 0;
        TipoTransacao = string.Empty;
    }
}
