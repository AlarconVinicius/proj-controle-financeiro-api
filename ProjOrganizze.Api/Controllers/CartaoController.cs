using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ProjOrganizze.Api.Dominio.DTOs.Cartao;
using ProjOrganizze.Api.Dominio.Entidades;
using ProjOrganizze.Api.Dominio.Interfaces.Services;
using ProjOrganizze.Api.Exceptions;
using ProjOrganizze.Api.Extensions;

namespace ProjOrganizze.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartaoController : MainController
    {
        private readonly ICartaoService _cartaoService;
        private readonly IValidator<CartaoAddDTO> _addValidator;
        private readonly IValidator<CartaoUpdDTO> _updValidator;
        public CartaoController(ICartaoService cartaoService, IValidator<CartaoAddDTO> addValidator, IValidator<CartaoUpdDTO> updValidator)
        {
            _cartaoService = cartaoService;
            _addValidator = addValidator;
            _updValidator = updValidator;
        }
        [HttpPost]
        public async Task<IActionResult> AdicionarCartao(CartaoAddDTO objeto)
        {
            var validationResult = await _addValidator.ValidateAsync(objeto);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    AdicionarErroProcessamento(error.ErrorMessage);
                }

                return CustomResponse();
            }
            Cartao objetoMapeado = objeto.ToAddDTO();
            try
            {
                await _cartaoService.AdicionarCartao(objetoMapeado);
            }
            catch (ServiceException ex)
            {
                AdicionarErroProcessamento(ex.Message);
                return CustomResponse();
            }
            var objetoMapeadoView = objetoMapeado.ToGetDTO();
            return CustomResponse(objetoMapeadoView);
        }

        [HttpGet]
        public async Task<IActionResult> ListarCartoes()
        {
            IEnumerable<Cartao> objetosDb = await _cartaoService.ObterCartoes();
            IEnumerable<CartaoViewDTO> objetosMapeados = objetosDb.Select(x => x.ToGetDTO());
            return CustomResponse(objetosMapeados);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterCartaoId([FromRoute] int id)
        {
            try
            {
                var objetoDb = await _cartaoService.ObterCartaoPorId(id);
                var objetoMapeado = objetoDb.ToGetDetailsDTO();
                return CustomResponse(objetoMapeado);
            }
            catch (ServiceException ex)
            {
                AdicionarErroProcessamento(ex.Message);
                return CustomResponse();
            }
        }

        [HttpPut]
        public async Task<IActionResult> AtualizarCartao(CartaoUpdDTO objeto)
        {
            var validationResult = await _updValidator.ValidateAsync(objeto);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    AdicionarErroProcessamento(error.ErrorMessage);
                }

                return CustomResponse();
            }
            Cartao objetoMapeado = objeto.ToUpdDTO();
            try
            {
                await _cartaoService.AtualizarCartao(objetoMapeado);
                return CustomResponse();
            }
            catch (ServiceException ex)
            {
                AdicionarErroProcessamento(ex.Message);
                return CustomResponse();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarCartao([FromRoute] int id)
        {
            try
            {
                await _cartaoService.DeletarCartao(id);
                return CustomResponse();
            }
            catch (ServiceException ex)
            {
                AdicionarErroProcessamento(ex.Message);
                return CustomResponse();
            }
        }
    }
}
