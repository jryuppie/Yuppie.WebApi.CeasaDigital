using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Yuppie.WebApi.CeasaDigital.Domain.Interfaces;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Negociacao;

namespace Yuppie.WebApi.CeasaDigital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfertaController : ControllerBase
    {
        private IOfertaService _OfertaService;
        public OfertaController(IOfertaService OfertaService)
        {
            _OfertaService = OfertaService;
        }
        [Route("BuscarOfertas")]
        [HttpGet]
        public List<OfertaModel> BuscarOfertas()
        {            
            return _OfertaService.BuscarTodasOfertas();
        }
        [Route("BuscarOfertasPorVendedor")]
        [HttpGet]
        public List<OfertaModel> BuscarOfertasPorVendedor(int dias, int idVendedor)
        {
            return _OfertaService.BuscarOfertasComVencimentoEm(dias, idVendedor);
        }
    }
}
