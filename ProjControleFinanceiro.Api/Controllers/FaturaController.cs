using Microsoft.AspNetCore.Mvc;
using ProjControleFinanceiro.Domain.DTOs.Fatura;
using ProjControleFinanceiro.Domain.Extensions;
using ProjControleFinanceiro.Domain.Interfaces.Repositorios;

namespace ProjControleFinanceiro.Api.Controllers
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
