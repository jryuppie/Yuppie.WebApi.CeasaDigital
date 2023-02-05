using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yuppie.WebApi.Infra.Context;
using Yuppie.WebApi.Infra.Models.Negociacao;

namespace Yuppie.WebApi.Infra.Repository
{
    public interface IOfertaRepository
    {
        OfertaModel GetOfertaById(int id);
        IEnumerable<OfertaModel> GetAllOfertas();
        void AddOferta(OfertaModel usuario);
        void UpdateOferta(OfertaModel usuario);
        void DeleteOferta(int id);
    }

    public class OfertaRepository : IOfertaRepository
    {
        private readonly PostGreContext _dbContext;
        public OfertaRepository(PostGreContext context) => _dbContext = context;

        public OfertaModel GetOfertaById(int id)
        {
            return _dbContext.Ofertas.Find(id);
        }

        public IEnumerable<OfertaModel> GetAllOfertas()
        {
            return _dbContext.Ofertas.ToList();
        }

        public void AddOferta(OfertaModel oferta)
        {
            _dbContext.Ofertas.Add(oferta);
            _dbContext.SaveChanges();
        }

        public void UpdateOferta(OfertaModel oferta)
        {
            _dbContext.Ofertas.Update(oferta);
            _dbContext.SaveChanges();
        }

        public void DeleteOferta(int id)
        {
            var oferta = _dbContext.Ofertas.Find(id);
            _dbContext.Ofertas.Remove(oferta);
            _dbContext.SaveChanges();
        }
    }
}
