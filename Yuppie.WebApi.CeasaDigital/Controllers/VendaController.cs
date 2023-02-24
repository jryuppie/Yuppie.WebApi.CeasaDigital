using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yuppie.WebApi.CeasaDigital.Domain.Interfaces;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Formulario;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Negociacao;

namespace Yuppie.WebApi.CeasaDigital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendaController : ControllerBase
    {
        private IVendaService _vendaService;
        public VendaController(IVendaService vendaService)
        {
            _vendaService = vendaService;
        }
        [Route("vendas")]
        [HttpGet]
        public async Task<ObjectResult> TodasVendas()
        {
            return await _vendaService.BuscarTodasVendas();
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<ObjectResult> BuscarVendaPorId(int id)
        {
            return await _vendaService.BuscarVendaPorId(id);
        }

        [Route("vendedor/{id}")]
        [HttpGet]
        public async Task<ObjectResult> BuscarVendaPorIdVendedor(int id)
        {
            return await _vendaService.BuscarVendaPorIdVendedor(id);
        }

        [Route("comprador/{id}")]
        [HttpGet]
        public async Task<ObjectResult> BuscarVendaPorIdComprador(int id)
        {
            return await _vendaService.BuscarVendaPorIdComprador(id);
        }

        [Route("ExecutarVenda")]
        [HttpPost]
        public async Task<ObjectResult> ExecutarVenda([FromBody] CadastrarVendaFormulario oModel)
        {
            return await _vendaService.ExecutarVenda(oModel.idOferta, oModel.qtd, oModel.idComprador);            
        }

        [Route("cancelamento")]
        [HttpPost]
        public async Task<ObjectResult> CancelarVenda([FromBody] VendaFormulario oModel)
        {
            return await _vendaService.ProcessoCancelamento(oModel.idVenda, oModel.idUsuario );            
        }
    }
}
