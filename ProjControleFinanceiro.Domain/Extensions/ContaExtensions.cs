using ProjControleFinanceiro.Domain.DTOs.Conta;
using ProjControleFinanceiro.Entities.Entidades;

namespace ProjControleFinanceiro.Domain.Extensions
{
    public static class ContaExtensions
    {

        public static Conta ToAddDTO(this ContaAddDTO value)
        {
           return new Conta(value.Nome, value.TipoConta, value.Saldo);
        }
        public static Conta ToUpdDTO(this ContaUpdDTO value)
        {
            return new Conta(value.Id, value.Nome, value.TipoConta, 0);
        }
        public static ContaViewDTO ToGetDTO(this Conta value)
        {
            return new ContaViewDTO
            {
                Id = value.Id,
                Nome = value.Nome,
                Saldo = value.Saldo,
                TipoConta = value.TipoConta.ToString(),
                //TipoConta = value.TipoConta.GetDisplayName(),
            };
       
        }

    }
}
