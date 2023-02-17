using Microsoft.AspNetCore.Mvc;
using Yuppie.WebApi.CeasaDigital.Domain.Interfaces;
using Yuppie.WebApi.CeasaDigital.Domain.Models.UsuarioModel;

namespace Yuppie.WebApi.CeasaDigital.Controllers
{
    [Route("api")]
    [ApiController]
    public class LoginController : Controller
    {
        private IUsuarioService _usuarioService;
        public LoginController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        [Route("login")]
        [HttpGet]
        public UsuarioModel Login(string usuario = "17996128054", string senha= "Valornoob98")
        {
            var loginDb = _usuarioService.BuscarUsuarioLogin(usuario, senha);
            return loginDb;
        }
    }
}
