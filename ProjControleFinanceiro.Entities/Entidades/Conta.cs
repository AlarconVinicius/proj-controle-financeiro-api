using ProjControleFinanceiro.Entities.Entidades.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjControleFinanceiro.Entities.Entidades
{
    [Table("Contas")]
    public class Conta
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public TipoConta TipoConta { get; set; }
        public double Saldo { get; private set; }

        public Conta(string nome, TipoConta tipoConta, double saldo = 0)
        {
            Nome = nome;
            TipoConta = tipoConta;
            Saldo = saldo;
        }
        public Conta(int id, string nome, TipoConta tipoConta, double saldo):this(nome, tipoConta, saldo)
        {
            Id = id;
        }
        public void AdicionarSaldo(double valor)
        {
            Saldo += valor;
        }

        public void SubtrairSaldo(double valor)
        {
            Saldo -= valor;
        }
    }
}
