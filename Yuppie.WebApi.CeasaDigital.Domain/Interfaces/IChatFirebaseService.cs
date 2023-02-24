using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Yuppie.WebApi.CeasaDigital.Domain.Interfaces
{
    public  interface IChatFirebaseService
    {
        public Task<ObjectResult> IniciaValidacaoLogin(int idChat);
        public void Login();    
        public void BuscarUsuarioPorId(int id);
        public void BuscarContratosPorId(int id);
        public void AtualizarDadosChatUsuario();
    }
}
