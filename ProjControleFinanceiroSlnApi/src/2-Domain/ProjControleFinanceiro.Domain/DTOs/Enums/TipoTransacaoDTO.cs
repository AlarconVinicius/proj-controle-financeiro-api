namespace ProjControleFinanceiro.Domain.DTOs.Enums
{
    public class TipoTransacaoDTO
    {
        public int Id { get; set; }
        public string TipoTransacao { get; set; }
        public TipoTransacaoDTO()
        {
            Id = 0;
            TipoTransacao = string.Empty;
        }
    }
}
