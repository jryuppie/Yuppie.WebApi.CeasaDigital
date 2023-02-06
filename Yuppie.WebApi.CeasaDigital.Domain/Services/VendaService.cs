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
                return JsonConvert.DeserializeObject<List<VendaModel>>(JsonConvert.SerializeObject(_VendaRepository.GetAllVendas().ToList())); ;
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public void ExecutarVenda(int idOferta, int quantidade, int idComprador)
        {
            try
            {
                var transacao = new TransacaoTools(_OfertaRepository);
                var Oferta = _OfertaRepository.GetOfertaById(idOferta);
                if (Oferta != null && Oferta.qtd_disponivel > 0 && Oferta.qtd_disponivel > quantidade && Oferta.id_vendedor != idComprador)
                {                   
                    var Negociacao = _NegociacaoRepository.GetNegociacaoByInformations(idComprador, idOferta, NegociacaoStatusString.Andamento);
                    if (Negociacao == null)
                    {
                        var vlrTransacao = Convert.ToDecimal(Oferta.vl_un_medida * quantidade);
                        if (vlrTransacao > 0)
                        {
                            var novaVenda = new Infra.Models.Negociacao.VendaModel()
                            {
                                id_comprador = idComprador,
                                id_oferta = idOferta,
                                qtd_comprada = quantidade,
                                valor_transacao = vlrTransacao,
                                id_vendedor = Oferta.id_vendedor
                            };
                            _VendaRepository.AddVenda(novaVenda);
                        }
                        transacao.AtualizarQuantidadeOferta(quantidade, idOferta);
                    }
                }
            }
            catch (Exception ex)
            {
              
            }
        }
    }
}

