using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Yuppie.WebApi.CeasaDigital.Domain.Interfaces;
using Yuppie.WebApi.CeasaDigital.Domain.Models.UsuarioModel;

namespace Yuppie.WebApi.CeasaDigital.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuarioController : Controller
    {
        
        private IUsuarioService _pgBaseService;
        public UsuarioController( IUsuarioService pgBaseService)
        {            
            _pgBaseService = pgBaseService;
        }

        [Route("BuscarUsuarios")]
        [HttpGet]
        public List<UsuarioModel> BuscarUsuarios()
        {
            return _pgBaseService.BuscarUsuarios();
        }

        [Route("{documento}")]
        [HttpGet]
        public UsuarioModel BuscarUsuarioPorDocumento(string documento)
        {
            return _pgBaseService.BuscarUsuarioPorDocumento(documento);
        }

        [Route("id/{id}")]
        [HttpGet]
        public UsuarioModel BuscarUsuarioPorId(int id)
        {
            return _pgBaseService.BuscarUsuarioPorId(id);
        }

        [Route("cadastro")]
        [HttpPost]
        public UsuarioModel CadastrarUsuario([FromForm] object formModel)
        {
            UsuarioModel usuario = new UsuarioModel();
            return _pgBaseService.CadastrarUsuario(usuario);
        }

        [Route("atualizar")]
        [HttpPut]
        public UsuarioModel AtualizarUsuario([FromForm] object formModel)
        {
            UsuarioModel usuario = new UsuarioModel();
            return _pgBaseService.AtualizarUsuario(usuario);
        }
    }
}
