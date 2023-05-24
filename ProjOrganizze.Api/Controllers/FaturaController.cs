using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjOrganizze.Api.Banco.Repositorios;
using ProjOrganizze.Api.Dominio.DTOs.Conta;
using ProjOrganizze.Api.Dominio.DTOs.Fatura;
using ProjOrganizze.Api.Dominio.Interfaces.Repositorios;
using ProjOrganizze.Api.Mapeamentos;

namespace ProjOrganizze.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaturaController : ControllerBase
    {
        private readonly IFaturaRepository _faturaRepository;
        private readonly FaturaMapping _faturaMapping;
        public FaturaController(IFaturaRepository faturaRepository)
        {
            _faturaRepository = faturaRepository;
            _faturaMapping = new FaturaMapping();
        }
        [HttpGet]
        public async Task<IActionResult> ListarFaturas()
        {
            var objetosDb = await _faturaRepository.ObterFaturas();
            List<FaturaViewDTO> objetosMapeados = new List<FaturaViewDTO>();
            foreach (var objetoDb in objetosDb)
            {
                objetosMapeados.Add(_faturaMapping.MapToGetDTO(objetoDb));
            }
            return Ok(objetosMapeados);
        }
    }
}
