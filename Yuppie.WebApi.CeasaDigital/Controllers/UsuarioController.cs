using Microsoft.AspNetCore.Mvc;
using Yuppie.WebApi.CeasaDigital.Domain.Interfaces;
using Yuppie.WebApi.CeasaDigital.Domain.Models.UsuarioModel;

namespace Yuppie.WebApi.CeasaDigital.Controllers
{
    public class UsuarioController : Controller
    {
        
        private IUsuarioService _pgBaseService;
        public UsuarioController( IUsuarioService pgBaseService)
        {            
            _pgBaseService = pgBaseService;
        }
        [Route("BuscarUsuario")]
        [HttpGet]
        public UsuarioModel BuscarUsuario()
        {
            var userDB = _pgBaseService.BuscarUsuario("17996128054", "Valornoob98");
            return userDB;
        }
    }
}
