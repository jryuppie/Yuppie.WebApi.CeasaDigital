using System;
using System.Collections.Generic;
using System.Linq;
using Yuppie.WebApi.Infra.Context;
using Yuppie.WebApi.Infra.Models.Negociacao;
using System.Threading.Tasks;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Yuppie.WebApi.Infra.Repository
{
    public interface IOfertaRepository
    {
        OfertaModel BuscarOfertaPorId(int idOferta);
        List<OfertaModel> BuscarOfertaPorVendedor(int idVendedor);
        List<OfertaModel> BuscarTodasOfertas();
        List<OfertaModel> BuscarOfertasComVencimentoEm(int dias, int idVendedor);
        void UpdateOferta(OfertaModel usuario);
        void DeleteOferta(int id);
        public void AdicionarOferta(OfertaModel oferta);
        public  Task<ObjectResult> AdicionarOfertaAsync(OfertaModel oferta);
    }

    public class OfertaRepository : IOfertaRepository
    {
        private readonly PostGreContext _dbContext;
        public OfertaRepository(PostGreContext context) => _dbContext = context;

        public List<OfertaModel> BuscarOfertaPorVendedor(int idVendedor)
        {
            return _dbContext.Ofertas.Where(x => x.id_vendedor == idVendedor).ToList();
        }

        public List<OfertaModel> BuscarTodasOfertas()
        {
            return _dbContext.Ofertas.ToList();
        }

        public List<OfertaModel> BuscarOfertasComVencimentoEm(int diasAtras, int idVendedor)
        {
            var umaSemanaAtras = DateTime.Now.AddDays(-diasAtras);
            return _dbContext.Ofertas.Where(x => x.id_vendedor == idVendedor && x.update_date <= umaSemanaAtras).ToList();
        }

        public OfertaModel BuscarOfertaPorId(int idOferta)
        {
            return _dbContext.Ofertas.Where(x => x.id == idOferta).FirstOrDefault();
        }

        public void AdicionarOferta(OfertaModel oferta)
        {
            _dbContext.Ofertas.Add(oferta);
            _dbContext.SaveChanges();
        }
        public async Task<ObjectResult> AdicionarOfertaAsync(OfertaModel oferta)
        {
            try
            {
                _dbContext.Ofertas.Add(oferta);
                _dbContext.SaveChanges();
                return new ObjectResult(oferta)
                {
                    StatusCode = StatusCodes.Status201Created
                };
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message })
                {
                    StatusCode = 400
                };
            }

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
