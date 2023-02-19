using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Formulario;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Negociacao;

namespace Yuppie.WebApi.CeasaDigital.Domain.Interfaces
{
    public interface IOfertaService
    {
        public Task<ObjectResult> BuscarTodasOfertas();
        public Task<ObjectResult> BuscarOfertasPorVendedor(int idVendedor);
        public Task<ObjectResult> BuscarOfertasComVencimentoEm(int dias, int idVendedor);
        public Task<ObjectResult> CadastrarOferta(int idProduto, int idUnMedida, int idVendedor, int qtdDisponivel,
            float pesoUnMedida,
            float vlUnMedida);
        public Task<ObjectResult> FinalizarOferta(int idOferta);

        public Task<ObjectResult> AtivarOferta(int idOferta);

        public Task<ObjectResult> AtualizarOferta(AtualizarOfertaFormulario oferta);
    }
}
