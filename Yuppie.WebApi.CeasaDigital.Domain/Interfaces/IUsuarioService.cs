using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yuppie.WebApi.CeasaDigital.Domain.Models.UsuarioModel;

namespace Yuppie.WebApi.CeasaDigital.Domain.Interfaces
{
    public interface IUsuarioService
    {
        public Task<ObjectResult> BuscarUsuarioLogin(string user, string password);
        public Task<ObjectResult> BuscarUsuarios();
        public Task<ObjectResult> BuscarUsuarioPorDocumento(string documento);
        public Task<ObjectResult> BuscarUsuarioPorId(int id);
        public Task<ObjectResult> CadastrarUsuario(UsuarioModel usuario);
        public Task<ObjectResult> AtualizarUsuario(UsuarioModel usuario);
        public Task<ObjectResult> MudarStatusUsuario(UsuarioModel usuario);
        public Task<ObjectResult> RecuperarSenhaUsuario(UsuarioModel usuario);
    }
}
