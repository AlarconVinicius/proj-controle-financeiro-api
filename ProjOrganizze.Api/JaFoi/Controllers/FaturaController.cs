using Microsoft.AspNetCore.Mvc;
using ProjOrganizze.Api.Dominio.DTOs.Fatura;
using ProjOrganizze.Api.Dominio.Interfaces.Repositorios;
using ProjOrganizze.Api.Extensions;

namespace ProjOrganizze.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaturaController : MainController
    {
        private readonly IFaturaRepository _faturaRepository;
        public FaturaController(IFaturaRepository faturaRepository)
        {
            _faturaRepository = faturaRepository;
        }
        [HttpGet]
        public async Task<IActionResult> ListarFaturas()
        {
            var objetosDb = await _faturaRepository.ObterFaturas();
            List<FaturaViewDTO> objetosMapeados = new List<FaturaViewDTO>();
            foreach (var objetoDb in objetosDb)
            {
                objetosMapeados.Add(objetoDb.ToGetDTO());
                //objetosMapeados.Add(_faturaMapping.MapToGetDTO(objetoDb));
            }
            return Ok(objetosMapeados);
        }
    }
}
