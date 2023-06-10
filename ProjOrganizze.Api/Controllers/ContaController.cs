using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ProjOrganizze.Api.Dominio.DTOs.Cartao;
using ProjOrganizze.Api.Dominio.DTOs.Conta;
using ProjOrganizze.Api.Dominio.Entidades;
using ProjOrganizze.Api.Dominio.Interfaces.Repositorios;
using ProjOrganizze.Api.Dominio.Interfaces.Services;
using ProjOrganizze.Api.Extensions;
using ProjOrganizze.Api.Mapeamentos;

namespace ProjOrganizze.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaController : MainController
    {
        private readonly IContaRepository _contaRepository;
        private readonly IContaService _service;
        private readonly IValidator<ContaAddDTO> _Validator;


        public ContaController(IContaRepository contaRepository, IContaService service, IValidator<ContaAddDTO> validar)
        {
            _contaRepository = contaRepository;
            _service = service;
            _Validator = validar;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarConta(ContaAddDTO objeto)
        {

            var validationResult = await _Validator.ValidateAsync(objeto);

            if (!validationResult.IsValid) return CustomResponse(validationResult);

            Conta objetoMapeado = objeto.ToAddDTO();

            await _service.Adicionar(objetoMapeado);

            var objetoMapeadoView = objetoMapeado.ToContaViewDTO();

            return CustomResponse(objetoMapeadoView);
        }

        [HttpGet]
        public async Task<IActionResult> ListarContas()
        {
            IEnumerable<Conta> objetosDb = await _contaRepository.ListAsync();
            IEnumerable<ContaViewDTO> contasView = objetosDb.Select(x => x.ToContaViewDTO());
            return CustomResponse(contasView);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ContaId([FromRoute] int id)
        {
            var objetoDb = await _contaRepository.GetEntityByIdAsync(id);
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
            var result = await _service.AtualizarConta(objetoMapeado);
            return CustomResponse(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeltarConta([FromQuery] int id)
        {
            await _contaRepository.DeleteAsync(id);
            return CustomResponse();
        }
    }
}

