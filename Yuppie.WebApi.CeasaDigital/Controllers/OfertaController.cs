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
    public class OfertaController : ControllerBase
    {
        private IOfertaService _OfertaService;
        public OfertaController(IOfertaService OfertaService)
        {
            _OfertaService = OfertaService;
        }
        [Route("buscarOfertas")]
        [HttpGet]
        public async Task<ObjectResult> BuscarOfertas()
        {
            return await _OfertaService.BuscarTodasOfertas();
        }
        [Route("vendedor/vencendo/{idVendedor}/{dias}")]
        [HttpGet]
        public async Task<ObjectResult> BuscarOfertasComVencimentoEm(int dias, int idVendedor)
        {
            return await _OfertaService.BuscarOfertasComVencimentoEm(dias, idVendedor);
        }

        [Route("vendedor/{idVendedor}")]
        [HttpGet]
        public async Task<ObjectResult> BuscarOfertaPorVendedor(int idVendedor)
        {
            return await _OfertaService.BuscarOfertasPorVendedor(idVendedor);
        }     

        [Route("encerraOferta")]
        [HttpPatch]
        public async Task<ActionResult<OfertaModel>> FinalizarOfertas([FromBody]  EncerrarOfertaFormulario oModel)
        {
            return await _OfertaService.FinalizarOferta(oModel.Id);
        }
        [Route("ativaroferta")]
        [HttpPatch]
        public async Task<ActionResult<OfertaModel>> AtivarOferta([FromBody]  AtivarOfertaFormulario oModel)
        {
            return await _OfertaService.AtivarOferta(oModel.Id);
        }
        [Route("cadastrarOferta")]
        [HttpPost]
        public async Task<ObjectResult> CadastrarOfertas([FromBody] CadastroOfertaFormulario oModel)
        {
            return await _OfertaService.CadastrarOferta(oModel.IdProduto, oModel.IdUnMedida, oModel.IdVendedor, oModel.QtdDisponivel, oModel.PesoUnMedida, oModel.VlUnMedida);
        }

        [Route("atualizaferta")]
        [HttpPut]
        public async Task<ActionResult<OfertaModel>> AtualizarOferta([FromForm] AtualizarOfertaFormulario oModel)
        {
            return await _OfertaService.AtualizarOferta(oModel);
        }
    }
}
