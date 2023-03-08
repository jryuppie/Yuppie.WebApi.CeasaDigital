using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yuppie.WebApi.CeasaDigital.Domain.Interfaces;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Formulario;
using Yuppie.WebApi.CeasaDigital.Domain.Models.UsuarioModel;

namespace Yuppie.WebApi.CeasaDigital.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuarioController : Controller
    {
        
        private IUsuarioService _pgUsuarioService;
        public UsuarioController(IUsuarioService pgUsuarioService)
        {
            _pgUsuarioService = pgUsuarioService;
        }

        [Route("BuscarUsuarios")]
        [HttpGet]
        public async Task<ObjectResult> BuscarUsuarios()
        {
            return await _pgUsuarioService.BuscarUsuarios();
        }

        [Route("{documento}")]
        [HttpGet]
        public async Task<ObjectResult> BuscarUsuarioPorDocumento(string documento)
        {
            return await _pgUsuarioService.BuscarUsuarioPorDocumento(documento);
        }

        [Route("id/{id}")]
        [HttpGet]
        public async Task<ObjectResult> BuscarUsuarioPorId(int id)
        {
            return await _pgUsuarioService.BuscarUsuarioPorId(id);
        }

        [Route("cadastro")]
        [HttpPost]
        public async Task<ObjectResult> CadastrarUsuario([FromForm] UsuarioFormulario formModel)
        {
            UsuarioModel usuario = new UsuarioModel()
            {
                nome = formModel.Nome,
                sobrenome = formModel.Sobrenome,
                tipo_usuario = formModel.TipoUsuario,
                tipo_pessoa= formModel.TipoPessoa,
                telefone = formModel.Telefone,
                cep = formModel.Cep,
                documento = formModel.Documento,
                senha= formModel.Senha,
                latitude= formModel.Latitude,
                longitude= formModel.Longitude,
            };
            return await _pgUsuarioService.CadastrarUsuario(usuario);
        }

        [Route("atualizar")]
        [HttpPut]
        public async Task<ObjectResult> AtualizarUsuario([FromForm] UsuarioFormulario formModel)
        {
            UsuarioModel usuario = new UsuarioModel()
            {
                id= formModel.Id,   
                nome = formModel.Nome,
                sobrenome = formModel.Sobrenome,              
                telefone = formModel.Telefone,              
                senha = formModel.Senha,              
            };
            return await _pgUsuarioService.AtualizarUsuario(usuario);
        }

        [Route("status")]
        [HttpPatch]
        public async Task<ObjectResult> AtualizarStatusUsuario([FromForm] UsuarioStatusFormulario formModel)
        {
            return await _pgUsuarioService.AtualizarStatusUsuario(formModel.Documento, formModel.Status);
        }


        [Route("recuperarsenha")]
        [HttpPatch]
        public async Task<ObjectResult> RecuperarSenhaUsuario(string Documento, string Telefone)
        {
            return await _pgUsuarioService.RecuperarSenhaUsuario(Documento, Telefone);
        }
    }
}
