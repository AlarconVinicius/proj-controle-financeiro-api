using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjOrganizze.Api.Banco.Repositorios;
using ProjOrganizze.Api.Dominio.DTOs.Cartao;
using ProjOrganizze.Api.Dominio.Entidades;
using ProjOrganizze.Api.Dominio.Interfaces.Repositorios;
using ProjOrganizze.Api.Mapeamentos;
using System.Globalization;

namespace ProjOrganizze.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartaoController : MainController
    {
        private readonly ICartaoRepository _cartaoRepository;
        private readonly IFaturaRepository _faturaRepository;
        private readonly IContaRepository _contaRepository;
        private readonly CartaoMapping _cartaoMapping;
        public CartaoController(ICartaoRepository cartaoRepository, IContaRepository contaRepository, IFaturaRepository faturaRepository)
        {
            _cartaoRepository = cartaoRepository;
            _contaRepository = contaRepository;
            _faturaRepository = faturaRepository;
            _cartaoMapping = new CartaoMapping();
        }
        [HttpPost]
        public async Task<IActionResult> AdicionarCartao(CartaoAddDTO objeto)
        {


            var contaExiste = await _contaRepository.GetEntityByIdAsync(objeto.ContaId);
            if (contaExiste == null)
            {
                 AdicionarErroProcessamento("Conta invalida");
                 return CustomResponse();
                //throw new Exception("Conta não encontrada.");
            }

            Cartao objetoMapeado = _cartaoMapping.MapToAddDTO(objeto);
            await _cartaoRepository.AddAsync(objetoMapeado);
            await _faturaRepository.AdicionarFaturas(objetoMapeado);
            var objetoMapeadoView = _cartaoMapping.MapToGetDTO(objetoMapeado);

            return Ok(objetoMapeadoView);
        }

        [HttpGet]
        public async Task<IActionResult> ListarCartoes()
        {
            var objetosDb = await _cartaoRepository.ObterCartoes();
            List<CartaoViewDTO> objetosMapeados = new List<CartaoViewDTO>();
            foreach (var objetoDb in objetosDb)
            {
                objetosMapeados.Add(_cartaoMapping.MapToGetDTO(objetoDb));
            }
            return Ok(objetosMapeados);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterCartaoId([FromRoute] int id)
        {
            var objetoDb = await _cartaoRepository.ObterCartaoPorId(id);
            var objetoMapeado = _cartaoMapping.MapToGetDTO(objetoDb);
            return Ok(objetoMapeado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarCartoes([FromRoute] int id)
        {
            await _cartaoRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
