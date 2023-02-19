using System;
using Yuppie.WebApi.Infra.Repository;

namespace Yuppie.WebApi.CeasaDigital.Domain.Tools
{
    public class TransacaoTools
    {   
        private readonly IOfertaRepository _ofertaRepository;
        public TransacaoTools(IOfertaRepository ofertaRepository)
        {          
            _ofertaRepository = ofertaRepository;
        }

        public decimal AtribuirValorTransacao(int quantidade, int idOferta)
        {
            return Convert.ToDecimal(_ofertaRepository.BuscarOfertaPorId(idOferta).vl_un_medida * quantidade);
        }
        public void AtualizarQuantidadeOferta(int quantidade, int IdOferta)
        {
            var oferta = _ofertaRepository.BuscarOfertaPorId(IdOferta);
            if (oferta != null && oferta.qtd_disponivel >= quantidade)
            {
                oferta.qtd_disponivel -= quantidade;
                _ofertaRepository.UpdateOferta(oferta);
            }
            else
            {
                throw new Exception("Não foi possível atualizar a quantidade da oferta.");
            }
        }
    }
}
