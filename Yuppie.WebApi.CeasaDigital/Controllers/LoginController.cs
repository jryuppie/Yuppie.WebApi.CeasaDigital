using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Yuppie.WebApi.CeasaDigital.Domain.Interfaces;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Formulario;
using Yuppie.WebApi.CeasaDigital.Domain.Models.UsuarioModel;

namespace Yuppie.WebApi.CeasaDigital.Controllers
{
    [Route("api")]
    [ApiController]
    public class LoginController : Controller
    {
        private ILoginService _LoginService;
        public LoginController(ILoginService loginService)
        {
            _LoginService = loginService;   
        }
        [Route("login")]
        [HttpPost]
        public async Task<string> Login([FromBody] LoginFormulario lForm)
        {
            return await _LoginService.Login(lForm.Usuario, lForm.Senha);
        }
    }
}
