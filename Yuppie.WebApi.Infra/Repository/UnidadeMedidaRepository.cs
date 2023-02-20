using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuppie.WebApi.Infra.Context;
using Yuppie.WebApi.Infra.Models.Produto;

namespace Yuppie.WebApi.Infra.Repository
{
    public interface IUnidadeMedidaRepository
    {
        public Task<UnidadeMedidaModel> BuscarUnMedidaPorId(int id);
        public Task<List<UnidadeMedidaModel>> BuscarTodasUnMedidas();
        public Task<ObjectResult> AdicionarUnMedida(string unMedida);
        public Task<ObjectResult> AtualizarUnMedida(UnidadeMedidaModel unMedida);
        public Task<ObjectResult> ExcluirUnMedida(int id);
    }

    public class UnidadeMedidaRepository : IUnidadeMedidaRepository
    {
        private readonly PostGreContext _dbContext;
        public UnidadeMedidaRepository(PostGreContext context) => _dbContext = context;

        public async Task<UnidadeMedidaModel> BuscarUnMedidaPorId(int id)
        {
            try
            {
                return _dbContext.UnMedidas.Find(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<UnidadeMedidaModel>> BuscarTodasUnMedidas()
        {
            try
            {
                return _dbContext.UnMedidas.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ObjectResult> AdicionarUnMedida(string unMedida)
        {
            try
            {
                UnidadeMedidaModel medida = new UnidadeMedidaModel
                {
                    nome = unMedida,
                    create_date = DateTime.Now,
                    update_date = DateTime.Now
                };
                _dbContext.UnMedidas.Add(medida);
                _dbContext.SaveChanges();
                return new ObjectResult(medida)
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

        public async Task<ObjectResult> AtualizarUnMedida(UnidadeMedidaModel unMedida)
        {
            try
            {
                _dbContext.UnMedidas.Update(unMedida);
                _dbContext.SaveChanges();
                return new ObjectResult(unMedida)
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


        public async Task<ObjectResult> ExcluirUnMedida(int  IdUnMedida)
        {
            try
            {
                var unMedida = _dbContext.UnMedidas.Find(IdUnMedida);
                _dbContext.UnMedidas.Remove(unMedida);
                _dbContext.SaveChanges();
                return new ObjectResult(unMedida)
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