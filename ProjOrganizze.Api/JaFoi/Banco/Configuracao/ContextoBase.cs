using Microsoft.EntityFrameworkCore;
using ProjOrganizze.Api.Dominio.Entidades;

namespace ProjOrganizze.Api.Banco.Configuracao
{
    public class ContextoBase : DbContext
    {
        public ContextoBase(DbContextOptions<ContextoBase> options) : base(options)
        { }

        public DbSet<Conta> Contas { get; set; }
        public DbSet<Cartao> Cartoes { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }
        public DbSet<Fatura> Faturas { get; set; }
    }
}
