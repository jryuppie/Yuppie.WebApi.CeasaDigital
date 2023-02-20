using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Yuppie.WebApi.CeasaDigital.Domain.Interfaces;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Formulario;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Produto;

namespace Yuppie.WebApi.CeasaDigital.Controllers
{
    [Route("api/medidas")]
    [ApiController]
    public class UnMedidaController : Controller
    {

        private IUnMedidaService _pgUnMedidaService;
        public UnMedidaController(IUnMedidaService pgUnMedidaService)
        {
            _pgUnMedidaService = pgUnMedidaService;
        }


        [Route("CadastrarUnMedida")]
        [HttpPost]
        public async Task<ObjectResult> CadastrarUnMedida([FromForm] UnMedidaFormulario formModel)
        {
           return await _pgUnMedidaService.CadastrarUnMedida(formModel.Nome);
        }

        [Route("DeletarUnMedida/{id}")]
        [HttpPost]
        public async Task<ObjectResult> DeletarUnMedida(int id)
        {
            return await _pgUnMedidaService.DeletarUnMedida(id);
        }

        [Route("BuscarTodasUnMedidas")]
        [HttpGet]
        public async Task<ObjectResult> BuscarTodasUnMedidas()
        {
            return await _pgUnMedidaService.BuscarTodasUnMedidas();
        }
    }
}
