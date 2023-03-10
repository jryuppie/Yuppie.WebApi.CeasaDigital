using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
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
    public class OfertaController : ControllerBase
    {
        private IOfertaService _OfertaService;
        public OfertaController(IOfertaService OfertaService)
        {
            _OfertaService = OfertaService;
        }
        //[Authorize]
        [HttpGet]
        public async Task<ObjectResult> BuscarOfertas()
        {
            return await _OfertaService.BuscarTodasOfertas();
        }
        //[Authorize]
        [HttpGet]
        [Route("ativas")]
        public async Task<ObjectResult> BuscarOfertasAtivas()
        {
            return await _OfertaService.BuscarTodasOfertasAtivas();
        }
        //[Authorize]
        //TODO - TRANSFORMAR EM UMA CONTROLLER DE VENCIMENTO.
        [Route("vendedor/vencendo/{idVendedor}/{dias}")]
        [HttpGet]
        public async Task<ObjectResult> BuscarOfertasComVencimentoEm(int dias, int idVendedor)
        {
            return await _OfertaService.BuscarOfertasComVencimentoEm(dias, idVendedor);
        }
        //[Authorize]
        [Route("vendedor/{id}")]
        [HttpGet]
        public async Task<ObjectResult> BuscarOfertaPorVendedor(int id)
        {
            return await _OfertaService.BuscarOfertasPorVendedor(id);
        }

        //[Authorize]
        [Route("ativar")]
        [HttpPatch]
        public async Task<ActionResult<OfertaModel>> AtivarOferta([FromBody] AtivarOfertaFormulario oModel)
        {
            return await _OfertaService.AtivarOferta(oModel.Id);
        }
        //[Authorize]
        [HttpPost]
        public async Task<ObjectResult> CadastrarOfertas([FromBody] CadastroOfertaFormulario oModel)
        {
            return await _OfertaService.CadastrarOferta(oModel.IdProduto, oModel.IdUnMedida, oModel.IdVendedor, oModel.QtdDisponivel, oModel.PesoUnMedida, oModel.VlUnMedida);
        }
        //[Authorize]
        [HttpPut]
        public async Task<ActionResult<OfertaModel>> AtualizarOferta([FromBody] AtualizarOfertaFormulario oModel)
        {
            return await _OfertaService.AtualizarOferta(oModel);
        }
        //[Authorize]
        [HttpDelete]
        public async Task<ActionResult<OfertaModel>> FinalizarOfertas([FromBody] EncerrarOfertaFormulario oModel)
        {
            return await _OfertaService.FinalizarOferta(oModel.Id);
        }
    }
}
