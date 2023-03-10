

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yuppie.WebApi.CeasaDigital.Domain.Interfaces;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Formulario;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Negociacao;
using Yuppie.WebApi.Infra.Repository;



namespace Yuppie.WebApi.CeasaDigital.Domain.Services
{
    public class OfertaService : IOfertaService
    {
        private readonly IMapper _mapper;
        private readonly IOfertaRepository _OfertaRepository;
        public OfertaService(IMapper mapper, IOfertaRepository OfertaRepository)
        {
            _OfertaRepository = OfertaRepository;
            _mapper = mapper;
        }


        public async Task<ObjectResult> BuscarTodasOfertas()
        {
            try
            {
                var oferta = _mapper.Map<List<OfertaModel>>(await _OfertaRepository.BuscarTodasOfertas());
                return new ObjectResult(oferta)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (System.Exception ex)
            {
                return new ObjectResult(new { message = "Falha ao buscar as ofertas!" })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }


        public async Task<ObjectResult> BuscarTodasOfertasAtivas()
        {
            try
            {
                var oferta = _mapper.Map<List<OfertaModel>>(await _OfertaRepository.BuscarTodasOfertasAtivas());
                return new ObjectResult(oferta)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (System.Exception ex)
            {
                return new ObjectResult(new { message = "Falha ao buscar as ofertas!" })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<ObjectResult> BuscarOfertasComVencimentoEm(int dias, int idVendedor)
        {
            try
            {
                var oferta = _mapper.Map<List<OfertaModel>>(await _OfertaRepository.BuscarOfertasComVencimentoEm(dias, idVendedor));
                return new ObjectResult(oferta)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (System.Exception ex)
            {
                return new ObjectResult(new { message = "Falha ao buscar as ofertas!" })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<ObjectResult> BuscarOfertasPorVendedor(int idVendedor)
        {
            try
            {
                var oferta = _mapper.Map<List<OfertaModel>>(await _OfertaRepository.BuscarOfertaPorVendedor(idVendedor));
                return new ObjectResult(oferta)
                {
                    StatusCode = StatusCodes.Status200OK
                };

            }
            catch (System.Exception ex)
            {
                return new ObjectResult(new { message = "Falha ao buscar as ofertas!" })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }


        public async Task<ObjectResult> CadastrarOferta(int idProduto, int idUnMedida, int idVendedor, int qtdDisponivel,
            float pesoUnMedida,
            float vlUnMedida)
        {

            var vlKG = (vlUnMedida / pesoUnMedida);
            Yuppie.WebApi.Infra.Models.Negociacao.OfertaModel ofertaCriacao = new Yuppie.WebApi.Infra.Models.Negociacao.OfertaModel
            {
                IdProduto = idProduto,
                IdUnMedida = idUnMedida,
                IdVendedor = idVendedor,
                QtdDisponivel = qtdDisponivel,
                ValorKg = vlKG,
                ValorUnMedida = vlUnMedida,
                PesoUnMedida = pesoUnMedida,
                DataCriacao = DateTime.Now,
                DataAtualizacao = DateTime.Now,
                Status = true
            };


            var ofertas = await _OfertaRepository.BuscarOfertaPorVendedor(idVendedor);
            if (ofertas != null && !ofertas.Any(x => x.IdProduto == idProduto))
            {
                return await _OfertaRepository.AdicionarOfertaAsync(ofertaCriacao);
            }
            else
            {
                var mensagemErro = ofertas == null ? "Vendedor não encontrado." : "Oferta com mesmo produto já existente.";
                return new ObjectResult(new { message = mensagemErro })
                {
                    StatusCode = 400
                };
            }
        }


        public async Task<ObjectResult> FinalizarOferta(int idOferta)
        {
            try
            {
                var oferta = await _OfertaRepository.BuscarOfertaPorId(idOferta);
                if (oferta != null)
                {
                    oferta.Status = false;
                    return await _OfertaRepository.AtualizarOfertaAsync(oferta);
                }
                return new ObjectResult(new { message = $"Falha ao finalizar a oferta: {idOferta}" }) { StatusCode = 400 };
            }
            catch (Exception)
            {
                return new ObjectResult(new { message = "Falha ao finalizar a oferta." }) { StatusCode = 500 };
            }
        }


        public async Task<ObjectResult> AtivarOferta(int idOferta)
        {
            try
            {
                var oferta = await _OfertaRepository.BuscarOfertaPorId(idOferta);
                if (oferta != null)
                {
                    oferta.Status = true;
                    return await _OfertaRepository.AtualizarOfertaAsync(oferta);
                }

                return new ObjectResult(new { message = $"Falha ao ativar a oferta: {idOferta}." })
                {
                    StatusCode = 400
                };

            }
            catch (Exception)
            {
                return new ObjectResult(new { message = $"Falha ao ativar a oferta." })
                {
                    StatusCode = 500
                };
            }
        }


        public async Task<ObjectResult> AtualizarOferta(AtualizarOfertaFormulario oferta)
        {
            try
            {
                var ofertaAtualiza = await _OfertaRepository.BuscarOfertaPorId(oferta.id);
                if (ofertaAtualiza != null)
                {
                    ofertaAtualiza.QtdDisponivel = oferta.qtd;
                    ofertaAtualiza.ValorUnMedida = oferta.vlUnMedida;
                    ofertaAtualiza.DataAtualizacao = DateTime.Now;
                    return await _OfertaRepository.AtualizarOfertaAsync(ofertaAtualiza);
                }
                else
                {
                    return new ObjectResult(new { message = $"Falha ao atualizar a oferta: {oferta.id}." }) { StatusCode = 400 };
                }
            }
            catch (Exception)
            {
                return new ObjectResult(new { message = $"Falha ao atualizar a oferta." }) { StatusCode = 500 };
            }
        }
    }
}
