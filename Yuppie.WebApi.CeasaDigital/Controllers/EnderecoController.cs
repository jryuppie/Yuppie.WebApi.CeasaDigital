using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Yuppie.WebApi.CeasaDigital.Domain.Interfaces;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Endereco;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Formulario;

namespace Yuppie.WebApi.CeasaDigital.Controllers
{

    [Route("api/[Controller]")]
    [ApiController]
    [EnableCors]
    public class EnderecoController : Controller
    {
        private IEnderecoService _EnderecoService;
        public EnderecoController(IEnderecoService EnderecoService)
        {
            _EnderecoService = EnderecoService;
        }
        //[AllowAnonymous]
        [HttpGet]
        public async Task<ObjectResult> ListaEnderecos()
        {
            return await _EnderecoService.BuscarTodosEnderecos();
        }
        [HttpGet]
        [Route("{idUsuario}")]
        public async Task<ObjectResult> BuscarEnderecoPorIdUsuario(int idUsuario)
        {
            return await _EnderecoService.BuscarEnderecoPorIdUsuario(idUsuario);
        }
        [HttpPost]
        public async Task<ObjectResult> CadastrarEndereco([FromBody] EnderecoFormulario formModel)
        {           

            EnderecoModel endereco = new EnderecoModel()
            {
                IdUsuario = formModel.IdUsuario,
                Cep = formModel.Cep,
                Bairro = formModel.Bairro,
                Logradouro = formModel.Logradouro,
                Numero = formModel.Numero,
                Cidade = formModel.Cidade,
                UF = formModel.UF,
                Latitude = formModel.Latitude,
                Longitude = formModel.Longitude,
                Radius = formModel.Radius,
                Status = formModel.Status             
            };
            return await _EnderecoService.CadastrarEndereco(endereco);
        }
        [HttpPatch]
        public async Task<ObjectResult> AtualizarEndereco([FromBody] EnderecoFormulario formModel)
        {
            EnderecoModel endereco = new EnderecoModel()
            {
                IdUsuario = formModel.IdUsuario,
                Cep = formModel.Cep,
                Bairro = formModel.Bairro,
                Logradouro = formModel.Logradouro,
                Numero = formModel.Numero,
                Cidade = formModel.Cidade,
                UF = formModel.UF,
                Latitude = formModel.Latitude,
                Longitude = formModel.Longitude,
                Radius = formModel.Radius,
                Status = formModel.Status,
                Complemento = formModel.Complemento
            };
            return await _EnderecoService.AtualizarEndereco(endereco);
        }
        [HttpPatch]
        [Route("desativar/{idEndereco}")]
        public async Task<ObjectResult> DesabiltiarEndereco(int idEndereco)
        {
            return await _EnderecoService.DesabilitarEndereco(idEndereco);
        }

        [HttpDelete]
        [Route("{idEndereco}")]
        public async Task<ObjectResult> ExcluirEndereco(int idEndereco)
        {
            return await _EnderecoService.ExcluirEndereco(idEndereco);
        }
    }

}
