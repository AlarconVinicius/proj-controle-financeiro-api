using ProjOrganizze.Api.Banco.Configuracao;
using ProjOrganizze.Api.Dominio.Interfaces.Repositorios;

namespace ProjOrganizze.Api.Banco.Repositorios
{
    public class UnitOfWorkRepository : IUnitOfWorkRepository
    {
        private readonly ContextoBase _context;

        public UnitOfWorkRepository(ContextoBase context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
        public void Rollback()
        {
            //
        }
    }
}
