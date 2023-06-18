namespace ProjControleFinanceiro.Entities.Filtros
{
    public class FaturaFiltro
    {
        public int CartaoId { get; set; }
        public int Mes { get; set; }
        public int Ano { get; set; }
        public FaturaFiltro(int cartaoId, int mes, int ano)
        {
            CartaoId = cartaoId;
            Mes = mes;
            Ano = ano;
        }
    }
}
