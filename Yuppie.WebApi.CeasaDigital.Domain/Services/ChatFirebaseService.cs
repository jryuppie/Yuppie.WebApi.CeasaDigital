using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Yuppie.WebApi.CeasaDigital.Domain.Interfaces;
using FirebaseAdmin;

namespace Yuppie.WebApi.CeasaDigital.Domain.Services
{

    public class ChatFirebaseService : IChatFirebaseService
    {
        private readonly FirebaseApp _firestoreDb;

        public ChatFirebaseService(FirebaseApp firestoreDb)
        {
            _firestoreDb = firestoreDb;
        }
        
        public void Login()
        {

        }
        public async Task<ObjectResult> IniciaValidacaoLogin(int idChat)
        {         
            try
            {

                var database = _firestoreDb.            

                return  new ObjectResult(idChat)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
               return new ObjectResult(idChat)
                {
                    StatusCode = 500
                };
            }
        }
        public void BuscarUsuarioPorId(int id)
        { }
        public void BuscarContratosPorId(int id) { }
        public void AtualizarDadosChatUsuario() { }

    }
}
