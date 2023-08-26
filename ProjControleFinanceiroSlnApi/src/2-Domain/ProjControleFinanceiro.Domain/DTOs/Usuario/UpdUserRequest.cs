namespace ProjControleFinanceiro.Domain.DTOs.Usuario;
public record UpdUserRequest (Guid Id, string Name, string LastName, string Email, string PhoneNumber);