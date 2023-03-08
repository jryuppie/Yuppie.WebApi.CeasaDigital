using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yuppie.WebApi.CeasaDigital.Domain.Interfaces;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Produto;
using Yuppie.WebApi.Infra.Repository;

namespace Yuppie.WebApi.CeasaDigital.Domain.Services
{
    public class UnMedidaService : IUnMedidaService
    {
        private readonly IMapper _mapper;
        private readonly IUnidadeMedidaRepository _unMedidaService;
        public UnMedidaService(IUnidadeMedidaRepository unMedidaService, IMapper mapper)
        {
            _unMedidaService = unMedidaService;
            _mapper = mapper;
        }

        public async Task<ObjectResult> CadastrarUnMedida(string nomeUnMedida)
        {
            try
            {
                var unMedida = _mapper.Map<UnidadeMedidaModel>(await _unMedidaService.AdicionarUnMedida(nomeUnMedida));
                return new ObjectResult(unMedida)
                {
                    StatusCode = StatusCodes.Status201Created
                };
            }
            catch (System.Exception ex)
            {
                return new ObjectResult(new { message = $"Falha ao criar a unidade de medida: {nomeUnMedida}!" })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }


        public async Task<ObjectResult> DeletarUnMedida(int idUnMedida)
        {
            try
            {
                var unMedida = _mapper.Map<UnidadeMedidaModel>(await _unMedidaService.ExcluirUnMedida(idUnMedida));
                return new ObjectResult(unMedida)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (System.Exception ex)
            {
                return new ObjectResult(new { message = $"Falha ao deletar unidade de medida!" })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }


        public async Task<ObjectResult> BuscarTodasUnMedidas()
        {
            try
            {
                var unMedidas = _mapper.Map<List<UnidadeMedidaModel>>(await _unMedidaService.BuscarTodasUnMedidas());
                return new ObjectResult(unMedidas)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (System.Exception ex)
            {
                return new ObjectResult(new { message = $"Falha ao buscar as unidade de medida!" })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
    }
}
