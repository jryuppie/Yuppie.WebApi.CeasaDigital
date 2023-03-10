using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Yuppie.WebApi.CeasaDigital.Domain.Interfaces;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Negociacao;

namespace Yuppie.WebApi.CeasaDigital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class NegociacaoController : Controller
    {
        private INegociacaoService _pgNegociacaoService;
        public NegociacaoController(INegociacaoService pgNegociacaoService)
        {
            _pgNegociacaoService = pgNegociacaoService;
        }
        //[Authorize]
        [Route("venda/{id}")]
        [HttpGet]
        public async Task<ObjectResult> BuscarNegociacaoPorIdVenda(int id)
        {
            return await _pgNegociacaoService.BuscarNegociacaoPorIdVenda(id);
        }

        //TODO - CRIAR O PROCESSO DE CONCLUSÃO/EDIÇÃO/CANCELAMENTO DE NEGOCIACAO
    }
}
