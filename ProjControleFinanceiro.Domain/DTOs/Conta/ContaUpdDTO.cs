using ProjControleFinanceiro.Entities.Entidades.Enums;

namespace ProjControleFinanceiro.Domain.DTOs.Conta
{
    public class ContaUpdDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public TipoConta TipoConta { get; set; }
    }

}
