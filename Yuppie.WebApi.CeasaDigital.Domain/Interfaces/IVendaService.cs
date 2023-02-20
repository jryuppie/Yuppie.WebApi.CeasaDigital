using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Negociacao;

namespace Yuppie.WebApi.CeasaDigital.Domain.Interfaces
{
    public interface IVendaService
    {
        public List<VendaModel> BuscarTodasVendas();
        public void ExecutarVenda(int idOferta, int quantidade, int idComprador);
        public VendaModel BuscarVendaPorId(int id);
        public List<VendaModel> BuscarVendaPorIdVendedor(int id);
        public List<VendaModel> BuscarVendaPorIdComprador(int id);
        public Task<ObjectResult> ProcessoCancelamento(int IdVenda, int IdUsuario);
    }
}
