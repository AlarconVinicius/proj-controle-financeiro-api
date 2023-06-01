using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjOrganizze.Api.Banco.Repositorios;
using ProjOrganizze.Api.Dominio.DTOs.Cartao;
using ProjOrganizze.Api.Dominio.DTOs.Conta;
using ProjOrganizze.Api.Dominio.Entidades;
using ProjOrganizze.Api.Dominio.Interfaces.Repositorios;
using ProjOrganizze.Api.Dominio.Interfaces.Services;
using ProjOrganizze.Api.Extensions;
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
        private readonly ICartaoService _cartaoService;
        public CartaoController(ICartaoRepository cartaoRepository, IContaRepository contaRepository, IFaturaRepository faturaRepository, ICartaoService cartaoService)
        {
            _cartaoRepository = cartaoRepository;
            _contaRepository = contaRepository;
            _faturaRepository = faturaRepository;
            _cartaoService = cartaoService;
        }
        [HttpPost]
        public async Task<IActionResult> AdicionarCartao(CartaoAddDTO objeto)
        {


            var contaExiste = await _contaRepository.GetEntityByIdAsync(objeto.ContaId);
            if (contaExiste == null)
            {
                 AdicionarErroProcessamento("Conta inválida");
                 return CustomResponse();
            }

            Cartao objetoMapeado = objeto.ToAddDTO();
            await _cartaoService.AdicionarCartao(objetoMapeado);
            await _faturaRepository.AdicionarFaturas(objetoMapeado); 
            // Adicionar await _unitOfWorkRepository.Commit();
            var objetoMapeadoView = objetoMapeado.ToGetDTO();

            return CustomResponse(objetoMapeadoView);
        }

        [HttpGet]
        public async Task<IActionResult> ListarCartoes()
        {
            var objetosDb = await _cartaoRepository.ObterCartoes();
            List<CartaoViewDTO> objetosMapeados = new List<CartaoViewDTO>();
            foreach (var objetoDb in objetosDb)
            {
                objetosMapeados.Add(objetoDb.ToGetDTO());
            }
            return CustomResponse(objetosMapeados);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterCartaoId([FromRoute] int id)
        {
            var objetoDb = await _cartaoRepository.ObterCartaoPorId(id);
            if (objetoDb == null)
            {
                AdicionarErroProcessamento("Cartão não localizada");
                return CustomResponse();

            }
            var objetoMapeado = objetoDb.ToGetDetailsDTO();
            return CustomResponse(objetoMapeado);
        }

        [HttpPut]
        public async Task<IActionResult> AtualizarCartao(CartaoUpdDTO objeto)
        {
            Cartao objetoMapeado = objeto.ToUpdDTO();
            var result = await _cartaoService.AtualizarCartao(objetoMapeado);
            return CustomResponse();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarCartao([FromRoute] int id)
        {
            await _cartaoRepository.DeleteAsync(id);
            return CustomResponse();
        }
    }
}
