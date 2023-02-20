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
        public List<VendaModel> TodasVendas()
        {
            return _vendaService.BuscarTodasVendas();
        }

        [Route("{id}")]
        [HttpGet]
        public VendaModel BuscarVendaPorId(int id)
        {
            return _vendaService.BuscarVendaPorId(id);
        }

        [Route("vendedor/{id}")]
        [HttpGet]
        public List<VendaModel> BuscarVendaPorIdVendedor(int id)
        {
            return _vendaService.BuscarVendaPorIdVendedor(id);
        }

        [Route("comprador/{id}")]
        [HttpGet]
        public List<VendaModel> BuscarVendaPorIdComprador(int id)
        {
            return _vendaService.BuscarVendaPorIdComprador(id);
        }

        [Route("ExecutarVenda")]
        [HttpPost]
        public bool ExecutarVenda(int idOferta, int quantidade, int idComprador)
        {
            _vendaService.ExecutarVenda(idOferta, quantidade, idComprador);
            return true;
        }

        [Route("cancelamento")]
        [HttpPost]
        public async Task<ObjectResult> CancelarVenda([FromBody] VendaFormulario oModel)
        {
            return await _vendaService.ProcessoCancelamento(oModel.idVenda, oModel.idUsuario );            
        }
    }
}
