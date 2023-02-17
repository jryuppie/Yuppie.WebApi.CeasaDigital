using System.Collections.Generic;
using Yuppie.WebApi.CeasaDigital.Domain.Models.UsuarioModel;

namespace Yuppie.WebApi.CeasaDigital.Domain.Interfaces
{
    public interface IUsuarioService
    {
        public UsuarioModel BuscarUsuarioLogin(string user, string password);
        public List<UsuarioModel> BuscarUsuarios();
        public UsuarioModel BuscarUsuarioPorDocumento(string documento);
        public UsuarioModel BuscarUsuarioPorId(int id);
        public UsuarioModel CadastrarUsuario(UsuarioModel usuario);
        public UsuarioModel AtualizarUsuario(UsuarioModel usuario);
        public UsuarioModel MudarStatusUsuario(UsuarioModel usuario);
        public UsuarioModel RecuperarSenhaUsuario(UsuarioModel usuario);

    }
}
