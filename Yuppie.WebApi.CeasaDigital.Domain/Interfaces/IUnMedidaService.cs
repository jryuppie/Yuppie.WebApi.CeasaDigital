using System;
using System.Collections.Generic;
using System.Text;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Produto;

namespace Yuppie.WebApi.CeasaDigital.Domain.Interfaces
{
    public interface IUnMedidaService
    {
        public bool CadastrarUnMedida(UnidadeMedidaModel unMedida);

        public bool AtualizaUnMedida(UnidadeMedidaModel unMedida);

        public bool DeletarUnMedida(UnidadeMedidaModel unMedida);
    }
}
