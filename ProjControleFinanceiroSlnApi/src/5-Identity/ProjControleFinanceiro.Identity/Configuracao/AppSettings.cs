namespace ProjControleFinanceiro.Identity.Configuracao;
public class AppSettings
{
    public required string Secret { get; set; }
    public required int ExpiracaoHoras { get; set; }
    public required string Emissor { get; set; }
    public required string ValidoEm { get; set; }

}
