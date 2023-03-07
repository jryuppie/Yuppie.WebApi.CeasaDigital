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
using Yuppie.WebApi.CeasaDigital.Domain.Models;

namespace Yuppie.WebApi.CeasaDigital.Domain.Services
{
    public class VendaService : IVendaService
    {
        private readonly IMapper _mapper;
        private readonly IVendaRepository _VendaRepository;
        private readonly IOfertaRepository _OfertaRepository;
        private readonly IProcessoNegociacaoRepository _NegociacaoRepository;
        private readonly IWhatsappService _WhatsappService;

        public VendaService(IVendaRepository VendaRepository, IOfertaRepository ofertaRepository, IProcessoNegociacaoRepository negociacaoRepository, IMapper mapper, IWhatsappService whatsappService)
        {
            _VendaRepository = VendaRepository;
            _OfertaRepository = ofertaRepository;
            _NegociacaoRepository = negociacaoRepository;
            _mapper = mapper;
            _WhatsappService = whatsappService;
            _WhatsappService = whatsappService;
        }

        public async Task<ObjectResult> BuscarTodasVendas()
        {
            try
            {
                var vendas = _mapper.Map<List<VendaModel>>(await _VendaRepository.BuscarTodasVendas());
                return new ObjectResult(vendas)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (System.Exception ex)
            {
                return new ObjectResult(new { message = $"Falha ao buscar vendas!" })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<ObjectResult> BuscarVendaPorId(int id)
        {
            try
            {
                var venda = _mapper.Map<VendaModel>(await _VendaRepository.BuscarVendaPorId(id));
                return new ObjectResult(venda)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (System.Exception ex)
            {
                return new ObjectResult(new { message = $"Falha ao buscar venda - id: {id}!" })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<ObjectResult> BuscarVendaPorIdVendedor(int id)
        {
            try
            {
                var venda = _mapper.Map<List<VendaModel>>(await _VendaRepository.BuscarVendaPorIdVendedor(id));
                return new ObjectResult(venda)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = $"Falha ao buscar venda por vendedor - id: {id}!" })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<ObjectResult> BuscarVendaPorIdComprador(int id)
        {
            try
            {
                var venda = _mapper.Map<List<VendaModel>>(await _VendaRepository.BuscarVendaPorIdComprador(id));
                return new ObjectResult(venda)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = $"Falha ao buscar venda por comprador - id: {id}!" })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<ObjectResult> ProcessoCancelamento(int IdVenda, int IdUsuario)
        {
            try
            {
                var venda = await _VendaRepository.BuscarVendaPorId(IdVenda);
                if (venda != null && (venda.id_comprador == IdUsuario || venda.id_vendedor == IdUsuario))
                {
                    var negociacao = await _NegociacaoRepository.BuscarNegociacaoPorVenda(IdVenda);
                    if (negociacao.status_negociacao != NegociacaoStatus.Processo.PegarDescricao())
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

                        if (negociacao.aprovacao_vendedor == true)
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

        public async Task<ObjectResult> ExecutarVenda(int idOferta, int quantidade, int idComprador)
        {
            try
            {
                var novaVenda = new Infra.Models.Negociacao.VendaModel();
                var transacao = new TransacaoTools(_OfertaRepository);
                var Oferta = await _OfertaRepository.BuscarOfertaPorId(idOferta);
                if (Oferta != null && Oferta.qtd_disponivel > 0 && Oferta.qtd_disponivel > quantidade && Oferta.id_vendedor != idComprador)
                {
                    var NegociacaoVenda = await _VendaRepository.BuscarVendaPorInformacoes(idComprador, idOferta, NegociacaoStatus.Processo.PegarDescricao());
                    if (NegociacaoVenda == null)
                    {
                        var vlrTransacao = Oferta.vl_un_medida * quantidade;
                        if (vlrTransacao > 0)
                        {
                            novaVenda = new Infra.Models.Negociacao.VendaModel()
                            {
                                id_comprador = idComprador,
                                id_oferta = idOferta,
                                qtd_comprada = quantidade,
                                valor_transacao = vlrTransacao,
                                id_vendedor = Oferta.id_vendedor,
                                create_date = DateTime.Now,
                                update_date = DateTime.Now,
                                venda_status = NegociacaoStatus.Processo.PegarDescricao()
                            };

                            //TODO - IMPLEMENTAR A ESTRUTURA DE CRIAÇÃO DE CONTRATO NO FIREBASE

                            //CRIA E ATUALIZA VENDA
                            await _VendaRepository.AdicionarVenda(novaVenda);
                            transacao.AtualizarQuantidadeOferta(quantidade, idOferta);

                            //CRIA NEGOCIAÇÃO
                            await IniciarNegociacao(idComprador, Oferta.id, quantidade);
                            _WhatsappService.EnviarMensagemOferta(
                               new MensagemModel()
                               {
                                   IdComprador = idComprador,
                                   IdVendedor = Oferta.id_vendedor,
                                   IdProduto = Oferta.id_produto,
                                   EnvioComprador = false
                               });
                        }
                    }
                }
                return new ObjectResult(novaVenda) { StatusCode = StatusCodes.Status201Created };
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = "Falha ao executar venda!" })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<ObjectResult> EditarVenda(int IdVenda, int Quantidade)
        {
            try
            {
                var venda = await _VendaRepository.BuscarVendaPorId(IdVenda);
                var oferta = await _OfertaRepository.BuscarOfertaPorId(venda.id_oferta);
                if (oferta != null && oferta.qtd_disponivel >= Quantidade)
                {
                    venda.qtd_comprada = Quantidade;
                    if (_VendaRepository.AtualizarVenda(venda).Result.StatusCode == 200)
                    {
                        var negociacao = await _NegociacaoRepository.BuscarNegociacaoPorVenda(venda.id);
                        if (negociacao != null)
                        {
                            negociacao.qtd_comprada = Quantidade;
                            if (_NegociacaoRepository.AtualizarNegociacao(negociacao).Result.StatusCode == 200)
                            {
                                _WhatsappService.EnviarMensagemVenda(new MensagemModel()
                                {
                                    IdComprador = venda.id_comprador,
                                    IdVendedor = venda.id_vendedor,
                                    IdOferta = oferta.id,
                                    Prefixo = "+55"
                                }, true);
                            }
                        }
                    }
                }
                return new ObjectResult(venda)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = $"Falha ao editar a venda - id: {IdVenda}!" })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
        private async Task<bool> IniciarNegociacao(int IdComprador, int IdOferta, int Quantidade)
        {
            try
            {
                var negociacao = await _VendaRepository.BuscarVendaPorInformacoes(IdComprador, IdOferta, NegociacaoStatus.Processo.PegarDescricao());
                if (negociacao != null)
                    await _NegociacaoRepository.AdicionarNegociacao(negociacao.id, Quantidade);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

