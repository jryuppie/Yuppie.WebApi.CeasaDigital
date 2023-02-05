using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Yuppie.WebApi.CeasaDigital.Domain.Interfaces;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Enums;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Negociacao;
using Yuppie.WebApi.Infra.Repository;

namespace Yuppie.WebApi.CeasaDigital.Domain.Services
{
    public class VendaService: IVendaService
    {
        private readonly IVendaRepository _VendaRepository;
        private readonly IOfertaRepository _OfertaRepository;
        private readonly IProcessoNegociacaoRepository _NegociacaoRepository;
        public VendaService(IVendaRepository VendaRepository, IOfertaRepository ofertaRepository, IProcessoNegociacaoRepository negociacaoRepository)
        {
            _VendaRepository = VendaRepository;
            _OfertaRepository = ofertaRepository;
            _NegociacaoRepository = negociacaoRepository;
        }

        public List<VendaModel> BuscarTodasVendas()
        {
            try
            {
                return JsonConvert.DeserializeObject<List<VendaModel>>(JsonConvert.SerializeObject(_VendaRepository.GetAllVendas())); ;
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public void ExecutarVenda(int idOferta, int idComprador, int quantidade)
        {
            try
            {
                var validaExistenciaOferta = _OfertaRepository.GetOfertaById(idOferta);
                if (validaExistenciaOferta != null && validaExistenciaOferta.qtd_disponivel > 0 && validaExistenciaOferta.id_vendedor != idComprador)
                {                   
                    var validaExistenciaNegociacao = _NegociacaoRepository.GetNegociacaoByInformations(idComprador, idOferta, NegociacaoStatusString.Andamento);
                    if (validaExistenciaNegociacao != null)
                    {
                        //TODO - CONTINUAR A MIGRAÇÃO
                    }
                }
            }
            catch (Exception ex)
            {
              
            }
        }
    }
}

