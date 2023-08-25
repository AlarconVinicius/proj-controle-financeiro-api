using ProjControleFinanceiro.Domain.DTOs.Usuario;
using ProjControleFinanceiro.Domain.Interfaces.Services;

namespace ProjControleFinanceiro.Identity.Domain.Interfaces;
public interface IAuthService : IMainService
{
    Task RegistrarUsuario(AddUserRequest objeto);
    Task<UsuarioRespostaLogin> LogarUsuario(LoginUserRequest objeto);
}