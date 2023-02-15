using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Yuppie.WebApi.CeasaDigital.Domain.Interfaces;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Enums;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Negociacao;
using Yuppie.WebApi.Infra.Repository;
using Yuppie.WebApi.CeasaDigital.Domain.Tools;
using System.Linq;

namespace Yuppie.WebApi.CeasaDigital.Domain.Services
{
    public class NegociacaoService : INegociacaoService
    {
        private readonly IProcessoNegociacaoRepository _negociacaoRepository;
        public NegociacaoService(IProcessoNegociacaoRepository negociacaoRepository)
        {
            _negociacaoRepository = negociacaoRepository;
        }

        public ProcessoNegociacaoModel BuscarNegociacao(int idVenda)
        {
            try
            {
                return JsonConvert.DeserializeObject<ProcessoNegociacaoModel>(JsonConvert.SerializeObject(_negociacaoRepository.GetNegociacaoById(idVenda)));
            }
            catch (System.Exception ex)
            {
            }
            return null;
        }
    }
}
