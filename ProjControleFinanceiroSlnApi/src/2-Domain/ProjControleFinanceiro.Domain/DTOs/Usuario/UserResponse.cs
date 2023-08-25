namespace ProjControleFinanceiro.Domain.DTOs.Usuario;
public record UserResponse(Guid Id, string Name, string LastName, string Email, string PhoneNumber, List<string> Roles);
