using Microsoft.AspNetCore.Mvc;
using Yuppie.WebApi.CeasaDigital.Domain.Interfaces;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Chat;

namespace Yuppie.WebApi.CeasaDigital.Controllers
{
    [Route("api/chat")]
    [ApiController]
    public class ChatFireBaseController : Controller
    {
        private IChatFirebaseService _pgChatFirebaseService;
        public ChatFireBaseController(IChatFirebaseService pgChatFirebaseService)
        {
            _pgChatFirebaseService = pgChatFirebaseService;
        }

        [Route("initLogin")]
        [HttpPost]
        public ChatFirebaseUserModel Login()
        {
            ChatFirebaseUserModel chat = new ChatFirebaseUserModel();
            _pgChatFirebaseService.IniciaValidacaoLogin(5);
            return chat;
        }

        [Route("user/{id}")]
        [HttpGet]
        public ChatFirebaseUserModel BuscarUsuarioPorId(int id)
        {
            ChatFirebaseUserModel chat = new ChatFirebaseUserModel();
            _pgChatFirebaseService.BuscarUsuarioPorId(id);
            return chat;
        }

        [Route("user/contacts/{id}")]
        [HttpGet]
        public bool BuscarContratosPorId(int id)
        {
            _pgChatFirebaseService.BuscarContratosPorId(id);
            return true;
        }

        [Route("user")]
        [HttpGet]
        public bool AtualizarDadosChatUsuario(int id)
        {
            _pgChatFirebaseService.BuscarContratosPorId(id);
            return true;
        }
    }
}
