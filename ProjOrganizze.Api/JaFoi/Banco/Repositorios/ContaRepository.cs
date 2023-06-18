using ProjOrganizze.Api.Banco.Configuracao;
using ProjOrganizze.Api.Dominio.Entidades;
using ProjOrganizze.Api.Dominio.Interfaces.Repositorios;

namespace ProjOrganizze.Api.Banco.Repositorios
{
    public class ContaRepository : BaseRepository<Conta>, IContaRepository
    {
        public ContaRepository(ContextoBase context) : base(context)
        {
        }
    }
}
