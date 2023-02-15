using Microsoft.AspNetCore.Mvc;
using Yuppie.WebApi.CeasaDigital.Domain.Interfaces;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Produto;

namespace Yuppie.WebApi.CeasaDigital.Controllers
{  

    public class UnMedidaController : Controller
    {

        private IUnMedidaService _pgUnMedidaService;
        public UnMedidaController(IUnMedidaService pgUnMedidaService)
        {
            _pgUnMedidaService = pgUnMedidaService;
        }


        [Route("CadastrarUnMedida")]
        [HttpGet]
        public void CadastrarUnMedida()
        {
           _pgUnMedidaService.CadastrarUnMedida(new UnidadeMedidaModel());            
        }

        [Route("DeletarUnMedida")]
        [HttpGet]
        public void DeletarUnMedida()
        {
            _pgUnMedidaService.DeletarUnMedida(new UnidadeMedidaModel());
        }

        [Route("AtualizaUnMedida")]
        [HttpGet]
        public void AtualizaUnMedida()
        {
            _pgUnMedidaService.AtualizaUnMedida(new UnidadeMedidaModel());
        }
    }
}
