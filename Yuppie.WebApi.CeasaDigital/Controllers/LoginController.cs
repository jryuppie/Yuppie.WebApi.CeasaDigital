using Microsoft.AspNetCore.Mvc;
using Yuppie.WebApi.CeasaDigital.Domain.Interfaces;
using Yuppie.WebApi.CeasaDigital.Domain.Models.UsuarioModel;

namespace Yuppie.WebApi.CeasaDigital.Controllers
{
    public class LoginController : Controller
    {
        private IBaseService _pgBaseService;
        public LoginController(IBaseService pgBaseService)
        {
            _pgBaseService = pgBaseService;
        }
        [Route("Login")]
        [HttpGet]
        public UsuarioModel Login(string usuario = "17996128054", string senha= "Valornoob98")
        {
            var loginDb = _pgBaseService.GetUser(usuario, senha);
            return loginDb;
        }
    }
}
