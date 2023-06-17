using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ProjOrganizze.Api.Dominio.DTOs.Cartao;
using ProjOrganizze.Api.Dominio.DTOs.Conta;
using ProjOrganizze.Api.Dominio.Entidades;
using ProjOrganizze.Api.Dominio.Interfaces.Repositorios;
using ProjOrganizze.Api.Dominio.Interfaces.Services;
using ProjOrganizze.Api.Extensions;
using ProjOrganizze.Api.Mapeamentos;
using ProjOrganizze.Api.Services;
using ProjOrganizze.Api.Validators.Conta;

namespace ProjOrganizze.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaController : MainController
    {
        private readonly IContaService _contaservice;
        private readonly IValidator<ContaAddDTO> _addValidator;
        private readonly IValidator<ContaUpdDTO> _updValidator;


        public ContaController(IContaRepository contaRepository, IContaService service, IValidator<ContaAddDTO> addValidator, IValidator<ContaUpdDTO> updValidator)
        {
            _contaservice = service;
            _addValidator = addValidator;
            _updValidator = updValidator;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarConta(ContaAddDTO objeto)
        {

            var validationResult = await _addValidator.ValidateAsync(objeto);

            if (!validationResult.IsValid) return CustomResponse(validationResult);

            Conta objetoMapeado = objeto.ToAddDTO();

            await _contaservice.Adicionar(objetoMapeado);

            var objetoMapeadoView = objetoMapeado.ToContaViewDTO();

            return CustomResponse(objetoMapeadoView);
        }

        [HttpGet]
        public async Task<IActionResult> ListarContas()
        {
            IEnumerable<Conta> objetosDb = await _contaservice.ListarContas();
            IEnumerable<ContaViewDTO> contasView = objetosDb.Select(x => x.ToContaViewDTO());
            return CustomResponse(contasView);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ContaId([FromRoute] int id)
        {
            var objetoDb = await _contaservice.ContaId(id);
            if(objetoDb == null)
            {
                AdicionarErroProcessamento("Conta não localizada");
                return CustomResponse();

            }
            var objetoMapeado = objetoDb.ToContaViewDTO();
            return CustomResponse(objetoMapeado);
        }

        [HttpPut]
        public async Task<IActionResult> AtualizarConta(ContaAddDTO objeto)
        {
            Conta objetoMapeado = objeto.ToAddDTO();
            var result = await _contaservice.AtualizarConta(objetoMapeado);
            return CustomResponse(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeltarConta([FromQuery] int id)
        {
            await _contaservice.DeletarConta(id);
            return CustomResponse();
        }
    }
}

