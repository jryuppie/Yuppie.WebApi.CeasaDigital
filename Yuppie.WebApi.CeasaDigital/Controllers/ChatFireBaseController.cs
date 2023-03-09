using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Yuppie.WebApi.CeasaDigital.Domain.Interfaces;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Chat;
using Yuppie.WebApi.CeasaDigital.Domain.Services;

namespace Yuppie.WebApi.CeasaDigital.Controllers
{
    [Route("api/chat")]
    [ApiController]
    [EnableCors]
    public class ChatFireBaseController : Controller
    {
        private IChatFirebaseService _pgChatFirebaseService;
        public ChatFireBaseController(IChatFirebaseService pgChatFirebaseService)
        {
            _pgChatFirebaseService = pgChatFirebaseService;
        }        

        [Route("user/{id}")]
        [HttpGet]
        public async Task<ObjectResult> BuscarUsuarioPorId(int id)
        {           
            return await _pgChatFirebaseService.BuscarUsuarioPorId(id);            
        }

        [Route("user/contacts/{id}")]
        [HttpGet]
        public async Task<ObjectResult> BuscarContratosPorId(int id)
        {
            return await _pgChatFirebaseService.BuscarContratosPorId(id);             
        }

        [Route("user")]
        [HttpGet]
        public bool AtualizarDadosChatUsuario(int id)
        {
            _pgChatFirebaseService.BuscarContratosPorId(id);
            return true;
        }

        [Route("initLogin")]
        [HttpPost]
        public async Task<ObjectResult> Login()
        {
            return await _pgChatFirebaseService.IniciaValidacaoLogin(8);
        }
    }
}
