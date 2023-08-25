using ProjControleFinanceiro.Data.Configuracao;
using ProjControleFinanceiro.Domain.Interfaces.Repositorios;
using ProjControleFinanceiro.Entities.Entidades;

namespace ProjControleFinanceiro.Data.Repositorios;

public class ClienteRepository : BaseRepository<Cliente>, IClienteRepository
{
    public ClienteRepository(ContextoBase context) : base(context)
    {
    }


}
