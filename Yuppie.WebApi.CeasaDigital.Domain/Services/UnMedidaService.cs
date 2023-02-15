using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Yuppie.WebApi.CeasaDigital.Domain.Interfaces;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Produto;
using Yuppie.WebApi.Infra.Repository;

namespace Yuppie.WebApi.CeasaDigital.Domain.Services
{
    public class UnMedidaService : IUnMedidaService
    {
        private readonly IUnidadeMedidaRepository _unMedidaService;
        public UnMedidaService(IUnidadeMedidaRepository unMedidaService)
        {
            _unMedidaService = unMedidaService;
        }

        public bool CadastrarUnMedida(UnidadeMedidaModel unMedida)
        {
            try
            {
                //var criacao = new Infra.Models.UnidadeMedidaModel();


                //var unMedSerialized = Yuppie.WebApi.Infra.Models.UnidadeMedidaModel(unMedida);
                //_unMedidaService.AddUnMedida(unMedSerialized);
            }
            catch (Exception ex)
            {
            }
            return false;
        }


        public bool DeletarUnMedida(UnidadeMedidaModel unMedida)
        {
            try
            {
                //var criacao = new Infra.Models.UnidadeMedidaModel();


                //var unMedSerialized = Yuppie.WebApi.Infra.Models.UnidadeMedidaModel(unMedida);
                //_unMedidaService.AddUnMedida(unMedSerialized);
            }
            catch (Exception ex)
            {
            }
            return false;
        }

        public bool AtualizaUnMedida(UnidadeMedidaModel unMedida)
        {
            try
            {
                //var criacao = new Infra.Models.UnidadeMedidaModel();


                //var unMedSerialized = Yuppie.WebApi.Infra.Models.UnidadeMedidaModel(unMedida);
                //_unMedidaService.AddUnMedida(unMedSerialized);
            }
            catch (Exception ex)
            {
            }
            return false;
        }
    }
}
