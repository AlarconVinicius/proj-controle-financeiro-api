using ProjControleFinanceiro.Entities.Entidades.Enums;

namespace ProjControleFinanceiro.Entities.Filtros
{
    public class TransacaoFiltro
    {
        public MetodoPagamento MetodoPagamento { get; set; }
        public TipoTransacao TipoTransacao { get; set; }
        public Categoria Categoria { get; set; }
    }
}
