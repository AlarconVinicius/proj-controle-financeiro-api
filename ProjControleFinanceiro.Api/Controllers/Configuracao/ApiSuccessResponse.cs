using System.ComponentModel;

namespace ProjControleFinanceiro.Api.Controllers.Configuracao
{
    public class ApiSuccessResponse<T>
    {
        public bool Success { get; set; }
        [DefaultValue(null)]
        public T Data { get; set; }
    }
}
