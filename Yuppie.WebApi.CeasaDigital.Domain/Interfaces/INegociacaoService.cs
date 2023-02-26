using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Negociacao;

namespace Yuppie.WebApi.CeasaDigital.Domain.Interfaces
{
    public interface INegociacaoService
    {
        public Task<ObjectResult> BuscarNegociacaoPorId(int IdVenda);
        public Task<ObjectResult> ProcessoConclusao(int IdVenda, int IdUsuario);
        public Task<ObjectResult> ProcessoCancelamento(int IdVenda, int IdUsuario);
        public Task<ObjectResult> CriarNegociacao(int IdVenda, int QuantidadeComprada);
    }
}
