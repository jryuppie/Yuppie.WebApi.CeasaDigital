using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yuppie.WebApi.CeasaDigital.Domain.Interfaces;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Formulario;
using Yuppie.WebApi.CeasaDigital.Domain.Models.UsuarioModel;

namespace Yuppie.WebApi.CeasaDigital.Controllers
{
    [Route("api/usuario/")]
    [ApiController]
    [EnableCors]
    //[Authorize]
    public class UsuarioController : Controller
    {

        private IUsuarioService _pgUsuarioService;
        public UsuarioController(IUsuarioService pgUsuarioService)
        {
            _pgUsuarioService = pgUsuarioService;
        }
         
        [HttpGet]
        public async Task<ObjectResult> BuscarUsuarios()
        {
            return await _pgUsuarioService.BuscarUsuarios();
        }
        
        [Route("documento/{documento}")]
        [HttpGet]
        public async Task<ObjectResult> BuscarUsuarioPorDocumento(string documento)
        {
            return await _pgUsuarioService.BuscarUsuarioPorDocumento(documento);
        }
         
        [Route("{id}")]
        [HttpGet]
        public async Task<ObjectResult> BuscarUsuarioPorId(int id)
        {
            return await _pgUsuarioService.BuscarUsuarioPorId(id);
        }

        [Route("avaliacao")]
        [HttpGet]
        public async Task<ObjectResult> BuscarUsuariosComAvaliacao()
        {
            return await _pgUsuarioService.BuscarUsuariosComAvaliacao();
        }
        [AllowAnonymous]
        [Route("senha")]
        [HttpPatch]
        public async Task<ObjectResult> RecuperarSenhaUsuario(string Documento, string Telefone)
        {
            return await _pgUsuarioService.RecuperarSenhaUsuario(Documento, Telefone);
        }
         
        [Route("status")]
        [HttpPatch]
        public async Task<ObjectResult> AtualizarStatusUsuario([FromBody] UsuarioStatusFormulario formModel)
        {
            return await _pgUsuarioService.AtualizarStatusUsuario(formModel.Documento, formModel.Status);
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<ObjectResult> CadastrarUsuario([FromBody] CadastrarUsuarioFormulario formModel)
        {            
            UsuarioModel usuario = new UsuarioModel()
            {
                Nome = formModel.nome,
                TipoPessoa = formModel.tipo_pessoa,
                Telefone = formModel.telefone,
                Documento = formModel.documento,
                Senha = formModel.senha
            };
            return await _pgUsuarioService.CadastrarUsuario(usuario);
        }
         
        [HttpPut]
        public async Task<ObjectResult> AtualizarUsuario([FromBody] UsuarioFormulario formModel)
        {
            UsuarioModel usuario = new UsuarioModel()
            {
                Id = formModel.Id,
                Nome = formModel.Nome,
                Sobrenome = formModel.Sobrenome,
                Telefone = formModel.Telefone,
                Senha = formModel.Senha,
            };
            return await _pgUsuarioService.AtualizarUsuario(usuario);
        }

    }
}
