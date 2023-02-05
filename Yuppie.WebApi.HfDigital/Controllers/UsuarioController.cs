using Microsoft.AspNetCore.Mvc;
using Yuppie.WebApi.CeasaDigital.Domain.Interfaces;
using Yuppie.WebApi.CeasaDigital.Domain.Models.UsuarioModel;

namespace Yuppie.WebApi.CeasaDigital.Controllers
{
    public class UsuarioController : Controller
    {
        
        private IBaseService _pgBaseService;
        public UsuarioController( IBaseService pgBaseService)
        {            
            _pgBaseService = pgBaseService;
        }
        [Route("GetUsers")]
        [HttpGet]
        public UsuarioModel GetUsers()
        {
            var userDB = _pgBaseService.GetUser("17996128054", "Valornoob98");
            return userDB;
        }
    }
}
