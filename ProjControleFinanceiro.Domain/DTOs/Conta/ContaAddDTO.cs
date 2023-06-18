using ProjControleFinanceiro.Entities.Entidades.Enums;

namespace ProjControleFinanceiro.Domain.DTOs.Conta
{
    public class ContaAddDTO
    {
        public string Nome { get; set; }
        public TipoConta TipoConta { get; set; }
        public double Saldo { get; set; }
    }

}
