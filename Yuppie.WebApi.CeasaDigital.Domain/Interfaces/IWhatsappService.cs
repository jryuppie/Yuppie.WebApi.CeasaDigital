using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yuppie.WebApi.CeasaDigital.Domain.Models;

namespace Yuppie.WebApi.CeasaDigital.Domain.Interfaces
{
    public interface IWhatsappService
    {
        public Task<ObjectResult> EnviarMensagem(MensagemModel model);
        public Task<ObjectResult> EnviarMensagemUsuario(int IdUsuario, bool RecuperarSenha);
    }
}
