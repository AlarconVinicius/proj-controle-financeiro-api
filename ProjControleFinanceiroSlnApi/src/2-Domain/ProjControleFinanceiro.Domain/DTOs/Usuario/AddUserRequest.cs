namespace ProjControleFinanceiro.Domain.DTOs.Usuario;
public record AddUserRequest(string Name, string LastName, string PhoneNumber, string Email, string Password, string ConfirmPassword);
