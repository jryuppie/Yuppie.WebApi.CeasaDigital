using Microsoft.AspNetCore.Mvc;
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
        public ProcessoNegociacaoModel BuscarNegociacao(int idVenda)
        {
            return _pgNegociacaoService.BuscarNegociacao(idVenda);
        }
    }
}
