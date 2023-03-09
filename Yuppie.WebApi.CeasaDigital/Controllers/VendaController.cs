using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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
    [EnableCors]
    public class VendaController : ControllerBase
    {
        private IVendaService _vendaService;
        public VendaController(IVendaService vendaService)
        {
            _vendaService = vendaService;
        }

        //[Authorize]
        [HttpGet]
        public async Task<ObjectResult> BuscarTodasVendas()
        {
            return await _vendaService.BuscarTodasVendas();
        }

        //[Authorize]
        [Route("{id}")]
        [HttpGet]
        public async Task<ObjectResult> BuscarVendaPorId(int id)
        {
            return await _vendaService.BuscarVendaPorId(id);
        }

        //[Authorize]
        [Route("vendedor/{id}")]
        [HttpGet]
        public async Task<ObjectResult> BuscarVendaPorIdVendedor(int id)
        {
            return await _vendaService.BuscarVendaPorIdVendedor(id);
        }

        //[Authorize]
        [Route("comprador/{id}")]
        [HttpGet]
        public async Task<ObjectResult> BuscarVendaPorIdComprador(int id)
        {
            return await _vendaService.BuscarVendaPorIdComprador(id);
        }

        //[Authorize]
        [Route("{id}")]
        [HttpPatch]
        public async Task<ObjectResult> EditarVenda([FromBody] EditarVendaFormulario oModel)
        {
            return await _vendaService.EditarVenda(oModel.idVenda, oModel.qtd);
        }

        //[Authorize]
        [HttpPost]
        public async Task<ObjectResult> ExecutarVenda([FromBody] CadastrarVendaFormulario oModel)
        {
            return await _vendaService.ExecutarVenda(oModel.idOferta, oModel.qtd, oModel.idComprador);            
        }

        //[Authorize]
        [Route("{id}")]
        [HttpDelete]
        public async Task<ObjectResult> CancelarVenda([FromBody] VendaFormulario oModel)
        {
            return await _vendaService.ProcessoCancelamento(oModel.idVenda, oModel.idUsuario );            
        }
    }
}
