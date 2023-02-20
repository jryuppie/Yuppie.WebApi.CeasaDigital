using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Yuppie.WebApi.CeasaDigital.Domain.Interfaces;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Negociacao;

namespace Yuppie.WebApi.CeasaDigital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NegociacaoController : Controller
    {
        private INegociacaoService _pgNegociacaoService;
        public NegociacaoController(INegociacaoService pgNegociacaoService)
        {
            _pgNegociacaoService = pgNegociacaoService;
        }

        [Route("{idVenda}")]
        [HttpGet]
        public async Task<ObjectResult> BuscarNegociacao(int idVenda)
        {
            return await _pgNegociacaoService.BuscarNegociacao(idVenda);
        }
    }
}
