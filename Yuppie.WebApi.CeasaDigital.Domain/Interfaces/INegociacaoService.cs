using System;
using System.Collections.Generic;
using System.Text;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Negociacao;

namespace Yuppie.WebApi.CeasaDigital.Domain.Interfaces
{
    public interface INegociacaoService
    {
        ProcessoNegociacaoModel BuscarNegociacao(int idVenda);
    }
}
