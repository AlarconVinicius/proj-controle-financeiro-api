namespace ProjControleFinanceiro.Domain.Interfaces.Services;

public interface IUser
{
    string Name { get; }
    Guid GetUserId();
    string GetUserEmail();
    bool IsAuthenticated { get; }
}
