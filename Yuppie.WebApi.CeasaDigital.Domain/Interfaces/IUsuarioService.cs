using Yuppie.WebApi.CeasaDigital.Domain.Models.UsuarioModel;

namespace Yuppie.WebApi.CeasaDigital.Domain.Interfaces
{
    public interface IUsuarioService
    {
        public UsuarioModel BuscarUsuario(string user, string password);
    }
}
