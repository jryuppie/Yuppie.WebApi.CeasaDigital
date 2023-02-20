using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Yuppie.WebApi.CeasaDigital.Domain.Interfaces;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Enums;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Negociacao;
using Yuppie.WebApi.Infra.Repository;
using Yuppie.WebApi.CeasaDigital.Domain.Tools;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using AutoMapper;

namespace Yuppie.WebApi.CeasaDigital.Domain.Services
{
    public class NegociacaoService : INegociacaoService
    {
        private readonly IMapper _mapper;
        private readonly IProcessoNegociacaoRepository _negociacaoRepository;
        public NegociacaoService(IProcessoNegociacaoRepository negociacaoRepository, IMapper mapper)
        {
            _negociacaoRepository = negociacaoRepository;
            _mapper = mapper;
        }

        public async Task<ObjectResult> BuscarNegociacao(int idVenda)
        {
            try
            {
                var oferta = _mapper.Map<ProcessoNegociacaoModel>(await _negociacaoRepository.BuscarNegociacaoPorId(idVenda));
                return new ObjectResult(oferta)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (System.Exception ex)
            {
                return new ObjectResult(new { message = "Falha ao buscar aa negociação!" })
                {
                    StatusCode = 500
                };
            }
        }
    }
}
