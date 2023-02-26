using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Yuppie.WebApi.CeasaDigital.Domain.Interfaces
{
    public interface IWhatsappService
    {
        public Task<ObjectResult> EnviarMensagem(int IdComprador, int IdVendedor, int IdProduto, string prefixo = "55");
    }
}
