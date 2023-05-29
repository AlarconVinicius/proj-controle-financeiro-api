using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjOrganizze.Api.Dominio.DTOs.Conta;
using ProjOrganizze.Api.Dominio.Entidades;
using ProjOrganizze.Api.Dominio.Interfaces.Repositorios;
using ProjOrganizze.Api.Dominio.Interfaces.Services;
using ProjOrganizze.Api.Mapeamentos;
using System.Collections.Generic;

namespace ProjOrganizze.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaController : MainController
    {
        private readonly IContaRepository _contaRepository;
        private readonly ContaMapping _contaMapping;
        private readonly IContaService _service;

        public ContaController(IContaRepository contaRepository, IContaService service)
        {
            _contaRepository = contaRepository;
            _contaMapping = new ContaMapping();
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarConta(ContaAddDTO objeto)
        {

            Conta objetoMapeado = _contaMapping.MapToAddDTO(objeto);

            await _service.Adicionar(objetoMapeado);
             
            var objetoMapeadoView = _contaMapping.MapToGetDTO(objetoMapeado);

            return CustomResponse(objetoMapeadoView);

        }

        [HttpGet]
        public async Task<IActionResult> ListarContas()
        {
            var objetosDb = await _contaRepository.ListAsync();
            List <ContaViewDTO> objetosMapeados = new List <ContaViewDTO>();
            foreach(var objetoDb in objetosDb)
            {
                objetosMapeados.Add(_contaMapping.MapToGetDTO(objetoDb));
            }
            return CustomResponse(objetosMapeados);
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
            var objetoMapeado = _contaMapping.MapToGetDTO(objetoDb);
            return CustomResponse(objetoMapeado);
        }

        [HttpPut]
        public async Task<IActionResult> AtualizarConta(ContaUpdDTO objeto)
        {
            var objetoMapeado = await _service.AtualizarConta(objeto);
            return CustomResponse(objetoMapeado);
        }

        [HttpDelete]
        public async Task<IActionResult> DeltarConta([FromQuery] int id)
        {
            await _contaRepository.DeleteAsync(id);
            return CustomResponse();
        }
    }
}

