using ProjControleFinanceiro.Domain.DTOs.Usuario;

namespace ProjControleFinanceiro.Identity.Domain.Interfaces;
public interface IUsuarioService
{
    Task<UserResponse> ObterUsuarioPorId(Guid id);
    Task<IEnumerable<UserResponse>> ObterUsuarios();
}
