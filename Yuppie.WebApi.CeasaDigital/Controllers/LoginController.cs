using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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
        public async Task<ObjectResult> Login(string usuario, string senha)
        {
            //ADAPTAR PARA A REQUISIÇÃO OAUTH.
           return await _usuarioService.BuscarUsuarioLogin(usuario, senha);          
        }
    }
}
