using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yuppie.WebApi.CeasaDigital.Domain.Interfaces;
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
        public async Task<ObjectResult> CadastrarUsuario([FromForm] object formModel)
        {
            UsuarioModel usuario = new UsuarioModel();
            return await _pgUsuarioService.CadastrarUsuario(usuario);
        }

        [Route("atualizar")]
        [HttpPut]
        public async Task<ObjectResult> AtualizarUsuario([FromForm] object formModel)
        {
            UsuarioModel usuario = new UsuarioModel();
            return await _pgUsuarioService.AtualizarUsuario(usuario);
        }
    }
}
