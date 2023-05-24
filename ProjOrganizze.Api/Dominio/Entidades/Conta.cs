using ProjOrganizze.Api.Dominio.Entidades.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjOrganizze.Api.Dominio.Entidades
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
