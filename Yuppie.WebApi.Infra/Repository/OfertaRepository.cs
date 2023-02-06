using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Common;
using Yuppie.WebApi.Infra.Context;
using Yuppie.WebApi.Infra.Models.Negociacao;
using Microsoft.EntityFrameworkCore;

namespace Yuppie.WebApi.Infra.Repository
{
    public interface IOfertaRepository
    {
        OfertaModel GetOfertaById(int id);
        IEnumerable<OfertaModel> GetAllOfertas();
        IEnumerable<OfertaModel> BuscarOfertasComVencimentoEm(int dias, int idVendedor);
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

        public IEnumerable<OfertaModel> BuscarOfertasComVencimentoEm(int diasAtras, int idVendedor)
        {
            var umaSemanaAtras = DateTime.Now.AddDays(-diasAtras);
            return _dbContext.Ofertas.Where(x => x.id_vendedor == idVendedor && x.update_date <= umaSemanaAtras).ToList();
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
