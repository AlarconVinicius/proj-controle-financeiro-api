using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ProjOrganizze.Api.Dominio.DTOs.Fatura;
using ProjOrganizze.Api.Dominio.DTOs.Transacao;
using ProjOrganizze.Api.Dominio.Entidades;
using ProjOrganizze.Api.Dominio.Entidades.Enums;
using ProjOrganizze.Api.Dominio.Interfaces.Repositorios;
using ProjOrganizze.Api.Dominio.Interfaces.Services;
using ProjOrganizze.Api.Exceptions;
using ProjOrganizze.Api.Extensions;
using ProjOrganizze.Api.Mapeamentos;

namespace ProjOrganizze.Api.Controllers
{
    [ApiController]
    [Route("api/transacoes")]
    public class TransacaoController : MainController
    {
        private readonly IBaseRepository<Conta> _contaRepository;
        private readonly IBaseRepository<Cartao> _cartaoRepository;
        private readonly ITransacaoRepository _transacaoRepository;
        private readonly ITransacaoService _transacaoService;
        private readonly IFaturaRepository _faturaRepository;
        private readonly TransacaoMapping _transacaoMapping;
        private readonly FaturaMapping _faturaMapping;
        private readonly IValidator<TransacaoAddDTO> _addValidator;
        public TransacaoController(IBaseRepository<Conta> contaRepository, ITransacaoRepository transacaoRepository, IBaseRepository<Cartao> cartaoRepository, IFaturaRepository faturaRepository, IValidator<TransacaoAddDTO> addValidator, ITransacaoService transacaoService)
        {
            _contaRepository = contaRepository;
            _transacaoRepository = transacaoRepository;
            _cartaoRepository = cartaoRepository;
            _faturaRepository = faturaRepository;
            _transacaoMapping = new TransacaoMapping();
            _faturaMapping = new FaturaMapping();
            _addValidator = addValidator;
            _transacaoService = transacaoService;
        }

        // Rota para adicionar uma nova transação
        [HttpPost]
        public async Task<IActionResult> AdicionarTransacao(TransacaoAddDTO objeto)
        {
            var validationResult = await _addValidator.ValidateAsync(objeto);
            if (!validationResult.IsValid) return CustomResponse(validationResult);
            Transacao objetoMapeado = objeto.ToAddDTO();
            try
            {
                await _transacaoService.AdicionarTransacao(objetoMapeado);
            }
            catch (ServiceException ex)
            {
                AdicionarErroProcessamento(ex.Message);
                return CustomResponse();
            }
            //var objetoMapeadoView = objetoMapeado.ToGetDTO();
            return CustomResponse(objeto);
        }
        [HttpGet]
        public async Task<IActionResult> ListarTransacoes()
        {
            var objetosDb = await _transacaoRepository.ObterTransacoes();
            List<TransacaoViewDTO> objetosMapeados = new List<TransacaoViewDTO>();
            foreach (var objetoDb in objetosDb)
            {
                objetosMapeados.Add(_transacaoMapping.MapToGetDTO(objetoDb));
            }
            return Ok(objetosMapeados);
        }
    }
}
