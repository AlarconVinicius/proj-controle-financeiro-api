using ProjControleFinanceiro.Domain.DTOs.Usuario;
using ProjControleFinanceiro.Domain.Interfaces.Services;

namespace ProjControleFinanceiro.Identity.Domain.Interfaces;
public interface IUsuarioService : IMainService
{
    Task<UserResponse> ObterUsuarioPorId(Guid id);
    Task<IEnumerable<UserResponse>> ObterUsuarios();
    Task AtualizarUsuario(UpdUserRequest objeto);
    Task AlterarStatusBloqueioUsuario(string userId, bool bloquear);
}
