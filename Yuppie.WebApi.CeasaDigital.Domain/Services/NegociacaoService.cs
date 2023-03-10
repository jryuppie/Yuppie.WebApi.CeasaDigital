using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Yuppie.WebApi.CeasaDigital.Domain.Interfaces;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Enums;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Negociacao;
using Yuppie.WebApi.Infra.Repository;

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
        public async Task<ObjectResult> CriarNegociacao(int IdVenda, int QuantidadeComprada)
        {
            try
            {
                var negociacao = new Infra.Models.Negociacao.ProcessoNegociacaoModel()
                {
                    IdVenda = IdVenda,
                    QtdComprada = QuantidadeComprada,
                    StatusNegociacao = NegociacaoStatus.Processo.BuscarDescricao(),
                    DataCriacao = DateTime.Now
                };
                return await _negociacaoRepository.AdicionarNegociacao(negociacao);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = "Falha ao criar a negociação!" })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
        public async Task<ObjectResult> BuscarNegociacaoPorId(int Id)
        {
            try
            {
                var oferta = _mapper.Map<ProcessoNegociacaoModel>(await _negociacaoRepository.BuscarNegociacaoPorId(Id));
                return new ObjectResult(oferta)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (System.Exception ex)
            {
                return new ObjectResult(new { message = "Falha ao buscar aa negociação!" })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<ObjectResult> BuscarNegociacaoPorIdVenda(int IdVenda)
        {
            try
            {
                var negociacao = _mapper.Map<ProcessoNegociacaoModel>(await _negociacaoRepository.BuscarNegociacaoPorIdVenda(IdVenda));
                return new ObjectResult(negociacao)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (System.Exception ex)
            {
                return new ObjectResult(new { message = "Falha ao buscar aa negociação!" })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
        public async Task<ObjectResult> ProcessoCancelamento(int IdVenda, int IdUsuario)
        {
            try
            {
                //ENDPOINT CRIADO NA CAMADA DE VENDA
                return new ObjectResult("Processo migrado para camada de Venda")
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (System.Exception ex)
            {
                return new ObjectResult(new { message = "Falha ao buscar aa negociação!" })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
        public async Task<ObjectResult> ProcessoConclusao(int IdVenda, int IdUsuario)
        {
            try
            {
                //ENDPOINT CRIADO NA CAMADA DE VENDA
                return new ObjectResult("Processo migrado para camada de Venda")
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (System.Exception ex)
            {
                return new ObjectResult(new { message = "Falha ao buscar aa negociação!" })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
    }
}
