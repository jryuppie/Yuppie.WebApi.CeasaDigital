using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Chat;

namespace Yuppie.WebApi.CeasaDigital.Domain.Interfaces
{
    public interface IChatFirebaseService
    {
        public Task<ObjectResult> IniciaValidacaoLogin(int IdChat);
        public void Login();
        public Task<ObjectResult> BuscarUsuarioPorId(int Id);
        public Task<ObjectResult> BuscarContratosPorId(int Id);
        public Task<ObjectResult> AtualizarDadosChatUsuario(ChatFirebaseUserModel model);
        public Task<ObjectResult> AdicionarContratos(int IdComprador, int IdVendedor);
    }
}
