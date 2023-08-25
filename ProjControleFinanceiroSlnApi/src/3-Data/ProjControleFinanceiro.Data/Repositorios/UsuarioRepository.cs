using ProjControleFinanceiro.Data.Configuracao;
using ProjControleFinanceiro.Domain.Interfaces.Repositorios;
using ProjControleFinanceiro.Entities.Entidades;

namespace ProjControleFinanceiro.Data.Repositorios;

public class UsuarioRepository : BaseRepository<Cliente>, IUsuarioRepository
{
    public UsuarioRepository(ContextoBase context) : base(context)
    { }


}
