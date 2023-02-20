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

namespace Yuppie.WebApi.CeasaDigital.Domain.Services
{
    public class VendaService : IVendaService
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
                var vendasDb = _VendaRepository.BuscarTodasVendas();
                return JsonConvert.DeserializeObject<List<VendaModel>>(JsonConvert.SerializeObject(vendasDb));
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public VendaModel BuscarVendaPorId(int id)
        {
            try
            {
                var vendaDb = _VendaRepository.BuscarVendaPorId(id);
                return JsonConvert.DeserializeObject<VendaModel>(JsonConvert.SerializeObject(vendaDb));
            }
            catch (Exception ex)
            {
            }
            return null;

        }


        public List<VendaModel> BuscarVendaPorIdVendedor(int id)
        {
            try
            {
                var vendasDb = _VendaRepository.BuscarVendaPorIdVendedor(id);
                return JsonConvert.DeserializeObject<List<VendaModel>>(JsonConvert.SerializeObject(vendasDb));
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public List<VendaModel> BuscarVendaPorIdComprador(int id)
        {
            try
            {
                var vendasDb = _VendaRepository.BuscarVendaPorIdComprador(id);
                return JsonConvert.DeserializeObject<List<VendaModel>>(JsonConvert.SerializeObject(vendasDb));
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public async Task<ObjectResult> ProcessoCancelamento(int IdVenda, int IdUsuario)
        {
            try
            {
                var venda = await _VendaRepository.BuscarVendaPorId(IdVenda);
                if (venda != null && (venda.id_comprador == IdUsuario || venda.id_vendedor == IdUsuario))
                {
                    var negociacao = await _NegociacaoRepository.BuscarNegociacaoPorVenda(IdVenda);
                    if (negociacao.status_negociacao != NegociacaoStatus.Andamento.PegarDescricao())
                    {
                        NegociacaoStatusResponse response = negociacao.status_negociacao == NegociacaoStatus.Cancelado.PegarDescricao() ? NegociacaoStatusResponse.Cancelado : NegociacaoStatusResponse.ConcluidoAnteriormente;
                        return new ObjectResult(new { message = response.PegarDescricao() }) { StatusCode = response.PegarCodigoStatus() };
                    }


                    if (venda.id_comprador == IdUsuario)
                    {
                        negociacao.aprovacao_comprador = true;
                        if (negociacao.aprovacao_comprador == false)
                        {
                            negociacao.sub_status_negociacao = NegociacaoSubStatusEnum.AguardandoAprovacaoConclusaoVendedor.PegarDescricao();
                            await _NegociacaoRepository.AtualizarNegociacao(negociacao);
                            NegociacaoStatusResponse response = NegociacaoStatusResponse.ConcluidoPendenteVendedor;
                            return new ObjectResult(new { message = response.PegarDescricao() }) { StatusCode = response.PegarCodigoStatus() };
                        }

                        if (negociacao.aprovacao_vendedor == true)
                        {
                            negociacao.sub_status_negociacao = NegociacaoSubStatusEnum.Concluido.PegarDescricao();
                            negociacao.status_negociacao = NegociacaoStatus.Concluido.PegarDescricao();
                            await _NegociacaoRepository.AtualizarNegociacao(negociacao);
                            venda.venda_status = NegociacaoStatus.Concluido.PegarDescricao();
                            await _VendaRepository.AtualizarVenda(venda);


                            //TODO - COLOCAR A LOGICA DE ENVIO DE MENSAGEM
                            NegociacaoStatusResponse response = NegociacaoStatusResponse.Concluido;
                            return new ObjectResult(new { message = response.PegarDescricao() }) { StatusCode = response.PegarCodigoStatus() };
                        }
                    }
                    else
                    {
                        negociacao.aprovacao_vendedor = true;
                        if (negociacao.aprovacao_comprador == false)
                        {
                            negociacao.sub_status_negociacao = NegociacaoSubStatusEnum.AguardandoAprovacaoCompradorCancelamento.PegarDescricao();
                            await _NegociacaoRepository.AtualizarNegociacao(negociacao);
                            NegociacaoStatusResponse response = NegociacaoStatusResponse.DesativadoPendenteComprador;
                            return new ObjectResult(new { message = response.PegarDescricao() }) { StatusCode = response.PegarCodigoStatus() };
                        }

                        if(negociacao.aprovacao_vendedor == true)
                        {
                            negociacao.sub_status_negociacao = NegociacaoSubStatusEnum.Cancelado.PegarDescricao();
                            negociacao.status_negociacao = NegociacaoStatus.Cancelado.PegarDescricao();
                            venda.venda_status = NegociacaoStatus.Cancelado.PegarDescricao();
                            //TODO - COLOCAR A LOGICA DE ENVIO DE MENSAGEM

                            await _NegociacaoRepository.AtualizarNegociacao(negociacao);
                            await _VendaRepository.AtualizarVenda(venda);

                            NegociacaoStatusResponse response = NegociacaoStatusResponse.Desativado;
                            return new ObjectResult(new { message = response.PegarDescricao() }) { StatusCode = response.PegarCodigoStatus() };
                        }
                    }
                }
                else
                {
                    NegociacaoStatusResponse response = NegociacaoStatusResponse.Inexistente;
                    var negociacao = await _NegociacaoRepository.BuscarNegociacaoPorVenda(IdVenda);
                    if (negociacao != null)                    
                        response = negociacao.status_negociacao == NegociacaoStatus.Concluido.PegarDescricao() ? 
                            NegociacaoStatusResponse.Concluido : NegociacaoStatusResponse.Cancelado;  
                  
                    return new ObjectResult(new { message = response.PegarDescricao() }) { StatusCode = response.PegarCodigoStatus() };
                }
                return null;
            }
            catch (System.Exception ex)
            {
                NegociacaoStatusResponse response = NegociacaoStatusResponse.ErroProcessamento;
                return new ObjectResult(new { message = response.PegarDescricao() }) { StatusCode = response.PegarCodigoStatus() };
            }
        }

        public async void ExecutarVenda(int idOferta, int quantidade, int idComprador)
        {
            try
            {
                var transacao = new TransacaoTools(_OfertaRepository);
                var Oferta = await _OfertaRepository.BuscarOfertaPorId(idOferta);
                if (Oferta != null && Oferta.qtd_disponivel > 0 && Oferta.qtd_disponivel > quantidade && Oferta.id_vendedor != idComprador)
                {
                    var Negociacao = _NegociacaoRepository.BuscarNegociacaoPorInformacoes(idComprador, idOferta, NegociacaoStatus.Andamento.PegarDescricao());
                    if (Negociacao == null)
                    {
                        var vlrTransacao = Oferta.vl_un_medida * quantidade;
                        if (vlrTransacao > 0)
                        {
                            //TODO -  VERIFICAR QUAL STATUS DEVE SER ATRIBUIDO 
                            var novaVenda = new Infra.Models.Negociacao.VendaModel()
                            {
                                id_comprador = idComprador,
                                id_oferta = idOferta,
                                qtd_comprada = quantidade,
                                valor_transacao = vlrTransacao,
                                id_vendedor = Oferta.id_vendedor,
                                create_date = DateTime.Now,
                                update_date = DateTime.Now,
                                venda_status = NegociacaoStatus.Andamento.PegarDescricao()
                            };
                            await _VendaRepository.AdicionarVenda(novaVenda);
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

