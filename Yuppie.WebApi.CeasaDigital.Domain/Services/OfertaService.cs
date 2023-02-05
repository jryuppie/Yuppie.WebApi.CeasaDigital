
using Newtonsoft.Json;
using System.Collections.Generic;
using Yuppie.WebApi.CeasaDigital.Domain.Interfaces;
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
                return JsonConvert.DeserializeObject<List<OfertaModel>>(JsonConvert.SerializeObject(_OfertaRepository.GetAllOfertas())); ;
            }
            catch (System.Exception ex)
            {
            }
            return null;
        }
    }
}
