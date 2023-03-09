using AutoMapper;
using System;
using System.Threading.Tasks;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Negociacao;
using Yuppie.WebApi.Infra.Repository;

namespace Yuppie.WebApi.CeasaDigital.Domain.Tools
{
    public class TransacaoTools
    {       
        private readonly IOfertaRepository _ofertaRepository;
        public TransacaoTools( IOfertaRepository ofertaRepository)
        {          
            _ofertaRepository = ofertaRepository;        
        }

        public async Task<decimal> AtribuirValorTransacao(int quantidade, int idOferta)
        {
            var oferta = await _ofertaRepository.BuscarOfertaPorId(idOferta);
            return Convert.ToDecimal(oferta.ValorUnMedida * quantidade);
        }
        public async void AtualizarQuantidadeOferta(int quantidade, int IdOferta)
        {
            var oferta = await _ofertaRepository.BuscarOfertaPorId(IdOferta);
            if (oferta != null && oferta.QtdDisponivel >= quantidade)
            {
                oferta.QtdDisponivel -= quantidade;
               await _ofertaRepository.AtualizarOfertaAsync(oferta);
            }
            else            
                throw new Exception("Não foi possível atualizar a quantidade da oferta.");
            
        }
    }
}
