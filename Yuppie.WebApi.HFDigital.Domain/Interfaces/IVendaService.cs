using System.Collections.Generic;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Negociacao;

namespace Yuppie.WebApi.CeasaDigital.Domain.Interfaces
{
    public interface IVendaService
    {
        public List<VendaModel> BuscarTodasVendas();
        public void ExecutarVenda(int idOferta, int idComprador, int quantidade);

    }
}
