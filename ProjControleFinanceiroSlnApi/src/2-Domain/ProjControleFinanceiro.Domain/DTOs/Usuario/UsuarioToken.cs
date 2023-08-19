namespace ProjControleFinanceiro.Domain.DTOs.Usuario;

public class UsuarioToken
{
    public string Id { get; set; }
    public string Email { get; set; }
    public IEnumerable<UsuarioClaim> Claims { get; set; }
}
