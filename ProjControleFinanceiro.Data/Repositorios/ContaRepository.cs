using ProjControleFinanceiro.Data.Configuracao;
using ProjControleFinanceiro.Entities.Entidades;
using ProjControleFinanceiro.Domain.Interfaces.Repositorios;

namespace ProjControleFinanceiro.Data.Repositorios
{
    public class ContaRepository : BaseRepository<Conta>, IContaRepository
    {
        public ContaRepository(ContextoBase context) : base(context)
        {
        }
    }
}
