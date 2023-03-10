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
        public Task<OfertaModel> BuscarOfertaPorId(int idOferta);
        public Task<List<OfertaModel>> BuscarOfertaPorVendedor(int idVendedor);
        public Task<List<OfertaModel>> BuscarTodasOfertas();
        public Task<List<OfertaModel>> BuscarTodasOfertasAtivas();
        public Task<List<OfertaModel>> BuscarOfertasComVencimentoEm(int dias, int idVendedor);                 
        public Task<ObjectResult> AdicionarOfertaAsync(OfertaModel oferta);
        public Task<ObjectResult> AtualizarOfertaAsync(OfertaModel oferta);
        public Task<ObjectResult> DeleteOfertaAsync(int idOferta);
    }

    public class OfertaRepository : IOfertaRepository
    {
        private readonly PostGreContext _dbContext;
        public OfertaRepository(PostGreContext context) => _dbContext = context;

        public async Task<List<OfertaModel>> BuscarOfertaPorVendedor(int idVendedor)
        {
            try
            {
                return _dbContext.Ofertas.Where(x => x.IdVendedor == idVendedor).ToList();              
            }
            catch (Exception ex)
            {
                throw;
            }           
        }

        public async Task<List<OfertaModel>> BuscarTodasOfertas()
        {
            try
            {
                return _dbContext.Ofertas.ToList();              
            }
            catch (Exception ex)
            {
                throw;
            }           
        }

        public async Task<List<OfertaModel>> BuscarTodasOfertasAtivas()
        {
            try
            {
                return _dbContext.Ofertas.Where(x=>x.Status == true).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<OfertaModel>> BuscarOfertasComVencimentoEm(int diasAtras, int idVendedor)
        {
            try
            {
                var umaSemanaAtras = DateTime.Now.AddDays(-diasAtras);
             return _dbContext.Ofertas.Where(x => x.IdVendedor == idVendedor && x.DataAtualizacao <= umaSemanaAtras).ToList();
             
            }
            catch (Exception ex)
            {
                throw;
            }           
        }

        public async Task<OfertaModel> BuscarOfertaPorId(int idOferta)
        {
            try
            {
                return _dbContext.Ofertas.Where(x => x.Id == idOferta).FirstOrDefault();              
            }
            catch (Exception ex)
            {
                throw;
            }
        }
       
        public async Task<ObjectResult> AdicionarOfertaAsync(OfertaModel oferta)
        {
            try
            {
                oferta.DataCriacao = DateTime.Now;
                _dbContext.Ofertas.Add(oferta);
                _dbContext.SaveChanges();
                return new ObjectResult(new { message = "Oferta criada com sucesso!" })
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

        public async Task<ObjectResult> AtualizarOfertaAsync(OfertaModel oferta)
        {
            try
            {
                _dbContext.Ofertas.Update(oferta);
                _dbContext.SaveChanges();
                return new ObjectResult(new { message = "Oferta atualizada com sucesso!" })
                {
                    StatusCode = StatusCodes.Status200OK
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

        public async Task<ObjectResult> DeleteOfertaAsync(int idOferta)
        {
            try
            {
                var oferta = _dbContext.Ofertas.Find(idOferta);
                _dbContext.Ofertas.Remove(oferta);
                _dbContext.SaveChanges();
                return new ObjectResult(oferta)
                {
                    StatusCode = StatusCodes.Status200OK
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
    }
}
