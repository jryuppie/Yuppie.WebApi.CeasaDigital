using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Negociacao;

namespace Yuppie.WebApi.CeasaDigital.Domain.Interfaces
{
    public interface IVendaService
    {
        public Task<ObjectResult> BuscarTodasVendas();
        public Task<ObjectResult> ExecutarVenda(int idOferta, int quantidade, int idComprador);
        public Task<ObjectResult> BuscarVendaPorId(int id);
        public Task<ObjectResult> BuscarVendaPorIdVendedor(int id);
        public Task<ObjectResult> BuscarVendaPorIdComprador(int id);
        public Task<ObjectResult> ProcessoCancelamento(int IdVenda, int IdUsuario);
    }
}
