namespace ProjControleFinanceiro.Domain.DTOs.Transacao;

public class TransacaoViewListDto
{
    public double Entrada { get; set; }
    public double Saida { get; set; }
    public double Saldo { get; set; }
    public IEnumerable<TransacaoViewDto> Transacoes { get; set; }

    public TransacaoViewListDto()
    {
        Transacoes= Enumerable.Empty<TransacaoViewDto>();
    }
}
