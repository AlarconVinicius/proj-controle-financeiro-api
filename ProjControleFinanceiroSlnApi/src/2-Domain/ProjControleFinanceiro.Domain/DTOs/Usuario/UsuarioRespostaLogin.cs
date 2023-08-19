namespace ProjControleFinanceiro.Domain.DTOs.Usuario;

public class UsuarioRespostaLogin
{
    public string AccessToken { get; set; }
    public double ExpiresIn { get; set; }
    public UsuarioToken UsuarioToken { get; set; }
}
