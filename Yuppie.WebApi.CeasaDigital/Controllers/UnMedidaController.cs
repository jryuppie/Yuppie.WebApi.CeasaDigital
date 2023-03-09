using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Yuppie.WebApi.CeasaDigital.Domain.Interfaces;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Formulario;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Produto;

namespace Yuppie.WebApi.CeasaDigital.Controllers
{
    [Route("api/medida")]
    [ApiController]
    [EnableCors]
    public class UnMedidaController : Controller
    {

        private IUnMedidaService _pgUnMedidaService;
        public UnMedidaController(IUnMedidaService pgUnMedidaService)
        {
            _pgUnMedidaService = pgUnMedidaService;
        }

        //[Authorize]
        [HttpGet]
        public async Task<ObjectResult> BuscarTodasUnMedidas()
        {
            return await _pgUnMedidaService.BuscarTodasUnMedidas();
        }

        //[Authorize]
        [HttpPost]
        public async Task<ObjectResult> CadastrarUnMedida([FromBody] UnMedidaFormulario formModel)
        {
            return await _pgUnMedidaService.CadastrarUnMedida(formModel.Nome);
        }

        //[Authorize]
        [HttpDelete]
        public async Task<ObjectResult> DeletarUnMedida(int id)
        {
            return await _pgUnMedidaService.DeletarUnMedida(id);
        }
    }
}
