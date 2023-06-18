using Microsoft.EntityFrameworkCore;

namespace ProjOrganizze.Api.Dominio.Interfaces.Repositorios
{
    public interface IUnitOfWorkRepository
    {
        public void Commit();
        public void Rollback();
    }
}