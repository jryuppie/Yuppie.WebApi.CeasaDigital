using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Yuppie.WebApi.CeasaDigital.Domain.Interfaces
{
    public interface ILoginService
    {
        public Task<string> Login(string Usuario, string Senha);
    }
}
