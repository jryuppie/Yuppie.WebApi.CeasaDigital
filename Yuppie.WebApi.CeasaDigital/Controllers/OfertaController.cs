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
        private IOfertaService _pgOfertaService;
        public OfertaController(IOfertaService pgOfertaService)
        {
            _pgOfertaService = pgOfertaService;
        }
        [Route("GetOffers")]
        [HttpGet]
        public List<OfertaModel> GetProducts()
        {            
            return _pgOfertaService.BuscarTodasOfertas();
        }
    }
}
