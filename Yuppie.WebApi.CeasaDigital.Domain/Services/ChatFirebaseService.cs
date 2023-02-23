using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuppie.WebApi.CeasaDigital.Domain.Interfaces;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Chat;
using Yuppie.WebApi.CeasaDigital.Domain.Models.UsuarioModel;
using Yuppie.WebApi.Infra.Context;

namespace Yuppie.WebApi.CeasaDigital.Domain.Services
{
    public class ChatFirebaseService : IChatFirebaseService
    {
        public void Login()
        {

        }
        public async Task<ObjectResult> IniciaValidacaoLogin(int idChat)
        {
           var retorno = new ObjectResult(idChat)
            {
                StatusCode = StatusCodes.Status200OK
            };
            try
            {

            }
            catch (Exception ex)
            {

                throw;
            }

            return retorno;
        }
        public void BuscarUsuarioPorId(int id)
        { }
        public void BuscarContratosPorId(int id) { }
        public void AtualizarDadosChatUsuario() { }

    }
}
