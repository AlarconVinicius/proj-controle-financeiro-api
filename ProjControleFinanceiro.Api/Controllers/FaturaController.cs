using Microsoft.AspNetCore.Mvc;
using ProjControleFinanceiro.Data.Repositorios;
using ProjControleFinanceiro.Domain.DTOs.Fatura;
using ProjControleFinanceiro.Domain.Extensions;
using ProjControleFinanceiro.Domain.Interfaces.Repositorios;
using ProjControleFinanceiro.Domain.Interfaces.Services;
using ProjControleFinanceiro.Entities.Entidades;
using ProjControleFinanceiro.Entities.Entidades.Enums;

namespace ProjControleFinanceiro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaturaController : MainController
    {
        private readonly IFaturaRepository _faturaRepository;
        private readonly ICartaoService _cartaoRepository;
        public FaturaController(IFaturaRepository faturaRepository, ICartaoService cartaoRepository)
        {
            _faturaRepository = faturaRepository;
            _cartaoRepository = cartaoRepository;
        }
        [HttpGet("cartao/{cartaoId}")]
        public async Task<IActionResult> ObterFaturas(int cartaoId)
        {
            var objetosDb = await _faturaRepository.ObterFaturas(cartaoId);
            List<FaturaViewDTO> objetosMapeados = new List<FaturaViewDTO>();
            //foreach (var objetoDb in objetosDb)
            //{
            //    objetosMapeados.Add(objetoDb.ToGetDTO());
            //}
            return CustomResponse(objetosDb.Select(x => x.ToGetDTO()));
        }
        [HttpGet("{faturaId}")]
        public async Task<IActionResult> ObterFaturaPorId(int faturaId)
        {
            var faturaDb = await _faturaRepository.ObterFaturaPorId(faturaId);
            if (faturaDb == null)
            {
                AdicionarErroProcessamento("Fatura não encontrada.");
                return CustomResponse();
            }
            return CustomResponse(faturaDb.ToGetDTO());
        }
        [HttpPost]
        public async Task<IActionResult> PagarFatura(int cartaoId, int faturaId)
        {
            var cartaoDb = await _cartaoRepository.ObterCartaoPorId(cartaoId);
            if (cartaoDb == null)
            {
                AdicionarErroProcessamento("Cartão não encontrado.");
                return CustomResponse();
            }
            var faturaDb = await _faturaRepository.ObterFaturaPorId(faturaId);
            if (faturaDb == null)
            {
                AdicionarErroProcessamento("Fatura não encontrada.");
                return CustomResponse();
            }
            if(faturaDb.StatusPagamento == StatusPagamento.Pago)
            {
                AdicionarErroProcessamento("Não é possível realizar o pagamento, pois a fatura já está paga.");
                return CustomResponse();
            }
            bool result = await _faturaRepository.PagarFatura(cartaoId, faturaId);
            if (!result)
            {
                AdicionarErroProcessamento("Falha ao tentar pagar fatura.");
                return CustomResponse();
            }
            return CustomResponse();
        }
    }
}
