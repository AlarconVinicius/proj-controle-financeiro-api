using ProjControleFinanceiro.Domain.DTOs.Cartao;

namespace ProjControleFinanceiro.Domain.Interfaces.Services
{
    public interface ICartaoService : IMainService
    {
        public Task<CartaoViewDTO> AdicionarCartao(CartaoAddDTO objeto);

        public Task<CartaoViewDTO> ObterCartaoPorId(int id);
        public Task<IEnumerable<CartaoViewDTO>> ObterCartoes();
        public Task<CartaoViewDTO> AtualizarCartao(CartaoUpdDTO objeto);
        public Task DeletarCartao(int id);
    }
}
