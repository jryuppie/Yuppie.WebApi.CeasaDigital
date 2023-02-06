using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Yuppie.WebApi.CeasaDigital.Domain.Interfaces;
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
        [Route("TodasVendas")]
        [HttpGet]
        public List<VendaModel> TodasVendas()
        {
            return _vendaService.BuscarTodasVendas();
        }

        [Route("ExecutarVenda")]
        [HttpPost]
        public bool ExecutarVenda(int idOferta, int quantidade, int idComprador)
        {
            _vendaService.ExecutarVenda(idOferta, quantidade, idComprador);
            return true;
        }
    }
}
