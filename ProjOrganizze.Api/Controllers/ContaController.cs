using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjOrganizze.Api.Dominio.DTOs.Conta;
using ProjOrganizze.Api.Dominio.Entidades;
using ProjOrganizze.Api.Dominio.Interfaces.Repositorios;
using ProjOrganizze.Api.Mapeamentos;
using System.Collections.Generic;

namespace ProjOrganizze.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        private readonly IContaRepository _contaRepository;
        private readonly ContaMapping _contaMapping;
        public ContaController(IContaRepository contaRepository)
        {
            _contaRepository = contaRepository;
            _contaMapping = new ContaMapping();
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarConta(ContaAddDTO objeto)
        {
            Conta objetoMapeado = _contaMapping.MapToAddDTO(objeto);
            await _contaRepository.AddAsync(objetoMapeado); 
            var objetoMapeadoView = _contaMapping.MapToGetDTO(objetoMapeado);
            return Ok(objetoMapeadoView);
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
            return Ok(objetosMapeados);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> ContaId([FromRoute] int id)
        {
            var objetoDb = await _contaRepository.GetEntityByIdAsync(id);
            if(objetoDb != null)
            {
                return NotFound();
            }
            var objetoMapeado = _contaMapping.MapToGetDTO(objetoDb);
            return Ok(objetoMapeado);
        }
        [HttpPut]
        public async Task<IActionResult> AtualizarConta(ContaUpdDTO objeto)
        {
            var objetoDb = await _contaRepository.GetEntityByIdAsync(objeto.Id);
            objetoDb.Nome = objeto.Nome;
            objetoDb.TipoConta = objeto.TipoConta;
            await _contaRepository.UpdateAsync(objetoDb);
            var objetoMapeado = _contaMapping.MapToGetDTO(objetoDb);
            return Ok(objetoMapeado);
        }
        [HttpDelete]
        public async Task<IActionResult> DeltarConta([FromQuery] int id)
        {
            await _contaRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
