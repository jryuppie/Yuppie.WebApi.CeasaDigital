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
        public Task<ObjectResult> EnviarMensagemVenda(MensagemModel model, bool EdicaoVenda);
        public Task<ObjectResult> EnviarMensagemNegociacao(MensagemModel model);
        public Task<ObjectResult> EnviarMensagemOferta(MensagemModel model);
        public Task<ObjectResult> EnviarMensagemUsuario(string Documento, bool RecuperarSenha);
    }
}
