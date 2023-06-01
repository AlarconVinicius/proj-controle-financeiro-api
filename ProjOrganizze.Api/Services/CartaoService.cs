using ProjOrganizze.Api.Dominio.Entidades;
using ProjOrganizze.Api.Dominio.Interfaces.Repositorios;
using ProjOrganizze.Api.Dominio.Interfaces.Services;

namespace ProjOrganizze.Api.Services
{
    public class CartaoService : ICartaoService
    {
        private readonly ICartaoRepository _cartaoRepository;
        public CartaoService(ICartaoRepository cartaoRepository)
        {
            _cartaoRepository = cartaoRepository;
        }

        public async Task AdicionarCartao(Cartao objeto)
        {
            await _cartaoRepository.AddAsync(objeto);
        }

        public async Task<Cartao> AtualizarCartao(Cartao objeto)
        {
            var objetoDb = await _cartaoRepository.GetEntityByIdAsync(objeto.Id);
            if(objetoDb == null)
            {

            }
            objetoDb.Nome = objeto.Nome;
            objetoDb.Limite = objeto.Limite;
            objetoDb.DiaVencimento = objeto.DiaVencimento;
            await _cartaoRepository.UpdateAsync(objetoDb);
            return objetoDb;
        }
    }
}
