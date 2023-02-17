using System;
using System.Collections.Generic;
using System.Text;

namespace Yuppie.WebApi.CeasaDigital.Domain.Interfaces
{
    public  interface IChatFirebaseService
    {
        public void Login();    
        public void BuscarUsuarioPorId(int id);
        public void BuscarContratosPorId(int id);
        public void AtualizarDadosChatUsuario();
    }
}
