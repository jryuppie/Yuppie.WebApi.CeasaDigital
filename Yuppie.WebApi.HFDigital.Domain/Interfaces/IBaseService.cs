using Yuppie.WebApi.CeasaDigital.Domain.Models.UsuarioModel;

namespace Yuppie.WebApi.CeasaDigital.Domain.Interfaces
{
    public interface IBaseService
    {
        public UsuarioModel GetUser(string user, string password);
    }
}
