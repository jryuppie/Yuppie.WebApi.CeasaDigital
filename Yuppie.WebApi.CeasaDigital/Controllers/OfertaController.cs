﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        [Route("buscarofertas")]
        [HttpGet]
        public List<OfertaModel> BuscarOfertas()
        {
            return _OfertaService.BuscarTodasOfertas();
        }
        [Route("vendedor/{idVendedor}")]
        [HttpGet]
        public List<OfertaModel> BuscarOfertasPorVendedor(int dias, int idVendedor)
        {
            return _OfertaService.BuscarOfertasComVencimentoEm(dias, idVendedor);
        }

        [Route("cadastraoferta")]
        [HttpPost]
        public async Task<ActionResult<OfertaModel>> CadastrarOfertas([FromForm] object formModel)
        {
            //verificar como esta sendo recebido o formulário
            var retorno = new OfertaModel();
            return retorno;
        }

        [Route("encerraoferta")]
        [HttpPost]
        public async Task<ActionResult<OfertaModel>> FinalizarOfertas([FromForm] object formModel)
        {
            //verificar como esta sendo recebido o formulário
            var retorno = new OfertaModel();
            return retorno;
        }
        [Route("ativaroferta")]
        [HttpPost]
        public async Task<ActionResult<OfertaModel>> AtivarOferta([FromForm] object formModel)
        {
            //verificar como esta sendo recebido o formulário
            var retorno = new OfertaModel();
            return retorno;
        }

        [Route("atualizaferta")]
        [HttpPost]
        public async Task<ActionResult<OfertaModel>> AtualizarOferta([FromForm] object formModel)
        {
            //verificar como esta sendo recebido o formulário
            var retorno = new OfertaModel();
            return retorno;
        }
    }
}
