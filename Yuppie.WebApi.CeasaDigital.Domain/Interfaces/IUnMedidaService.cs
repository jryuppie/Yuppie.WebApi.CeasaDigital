using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Produto;

namespace Yuppie.WebApi.CeasaDigital.Domain.Interfaces
{
    public interface IUnMedidaService
    {
       public Task<ObjectResult> CadastrarUnMedida(string nomeUnMedida);                   
       public Task<ObjectResult> DeletarUnMedida(int idUnMedida);
        public Task<ObjectResult> BuscarTodasUnMedidas();
    }
}
