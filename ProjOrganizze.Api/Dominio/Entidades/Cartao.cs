using System.ComponentModel.DataAnnotations.Schema;

namespace ProjOrganizze.Api.Dominio.Entidades
{
    [Table("Cartoes")]
    public class Cartao
    {
        public int Id { get; set; }
        [ForeignKey("Conta")]
        public int ContaId { get; set; }
        public string Nome { get; set; }
        public double Limite { get; set; }
        public double Saldo { get; private set; }
        public int DiaVencimento { get; set; }
        public int DiferencaDias { get; set; }
        public List<Fatura> Faturas { get; set; }
        public Conta? Conta { get; set; }

        public Cartao()
        {
            Faturas = new List<Fatura>();
        }
        public Cartao(int contaId, string nome, double limite, int diaVencimento, int diferencaDias, double saldo = 0) : this()
        {
            ContaId = contaId;
            Nome = nome;
            Limite = limite;
            DiaVencimento = diaVencimento;
            DiferencaDias = diferencaDias;
            Saldo = saldo;
        }
        public Cartao(int contaId, string nome, double limite, int diaVencimento, int diferencaDias, List<Fatura> faturas, double saldo = 0):this()
        {
            ContaId = contaId;
            Nome = nome;
            Limite = limite;
            DiaVencimento = diaVencimento;
            DiferencaDias = diferencaDias;
            Faturas = faturas;
            Saldo = saldo;
        }
        public void SubtrairSaldo(double valor)
        {
            Saldo -= valor;
        }
    }
}
