

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yuppie.WebApi.CeasaDigital.Domain.Interfaces;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Enums;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Negociacao;
using Yuppie.WebApi.Infra.Repository;


namespace Yuppie.WebApi.CeasaDigital.Domain.Services
{
    public class OfertaService : IOfertaService
    {
        private readonly IOfertaRepository _OfertaRepository;
        public OfertaService(IOfertaRepository OfertaRepository)
        {
            _OfertaRepository = OfertaRepository;
        }


        public List<OfertaModel> BuscarTodasOfertas()
        {
            try
            {
                return JsonConvert.DeserializeObject<List<OfertaModel>>(JsonConvert.SerializeObject(_OfertaRepository.BuscarTodasOfertas())); ;
            }
            catch (System.Exception ex)
            {
            }
            return null;
        }

        public List<OfertaModel> BuscarOfertasComVencimentoEm(int dias, int idVendedor)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<OfertaModel>>(JsonConvert.SerializeObject(_OfertaRepository.BuscarOfertasComVencimentoEm(dias, idVendedor))); ;
            }
            catch (System.Exception ex)
            {
            }
            return null;
        }


        public async Task<ObjectResult> CadastrarOferta(int idProduto, int idUnMedida, int idVendedor, int qtdDisponivel,
            float pesoUnMedida,
            float vlUnMedida)
        {

            var vlKG = (vlUnMedida / pesoUnMedida);
            Yuppie.WebApi.Infra.Models.Negociacao.OfertaModel ofertaCriacao = new Yuppie.WebApi.Infra.Models.Negociacao.OfertaModel
            {
                id_produto = idProduto,
                id_un_medida = idUnMedida,
                id_vendedor = idVendedor,
                qtd_disponivel = qtdDisponivel,
                vlkg = vlKG,
                vl_un_medida = vlUnMedida,
                peso_un_medida = pesoUnMedida,
                create_date = DateTime.Now,
                update_date = DateTime.Now,
                status = true
            };


            var ofertas = _OfertaRepository.BuscarOfertaPorVendedor(idVendedor);
            if (ofertas != null && !ofertas.Any(x => x.id_produto == idProduto))
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
    }
}
