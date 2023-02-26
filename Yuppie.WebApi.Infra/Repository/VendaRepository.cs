using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yuppie.WebApi.Infra.Context;
using Yuppie.WebApi.Infra.Models.Negociacao;


namespace Yuppie.WebApi.Infra.Repository
{
    public interface IVendaRepository
    {
        public Task<VendaModel> BuscarVendaPorId(int id);
        public Task<List<VendaModel>> BuscarVendaPorIdVendedor(int id);
        public Task<List<VendaModel>> BuscarVendaPorIdComprador(int id);
        public Task<List<VendaModel>> BuscarTodasVendas();
        public Task<ObjectResult> AdicionarVenda(VendaModel venda);
        public Task<ObjectResult> AtualizarVenda(VendaModel venda);
        public Task<ObjectResult> ExcluirVenda(int id);
        public Task<VendaModel> BuscarVendaPorInformacoes(int idComprador, int idVenda, string status)
    }

    public class VendaRepository : IVendaRepository
    {
        private readonly PostGreContext _dbContext;
        public VendaRepository(PostGreContext context) => _dbContext = context;

        public async Task<VendaModel> BuscarVendaPorId(int id)
        {
            try
            {
                return _dbContext.Vendas.Find(id);
            }
            catch (Exception ex)
            {
                throw;
            }           
        }


        public async Task<List<VendaModel>> BuscarTodasVendas()
        {
            try
            {
                return _dbContext.Vendas.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<VendaModel>> BuscarVendaPorIdVendedor(int id)
        {
            try
            {
                return _dbContext.Vendas.Where(x => x.id_vendedor == id).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<VendaModel>> BuscarVendaPorIdComprador(int id)
        {
            try
            {
                return _dbContext.Vendas.Where(x => x.id_comprador == id).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<ObjectResult> AdicionarVenda(VendaModel venda)
        {
            try
            {
                _dbContext.Vendas.Add(venda);
                _dbContext.SaveChanges();
                return new ObjectResult(venda)
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

        public async Task<ObjectResult> AtualizarVenda(VendaModel venda)
        {
            try
            {
                _dbContext.Vendas.Update(venda);
                _dbContext.SaveChanges();
                return new ObjectResult(venda)
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

        public async Task<ObjectResult> ExcluirVenda(int IdVenda)
        {
            try
            {
                var venda = _dbContext.Vendas.Find(IdVenda);
                _dbContext.Vendas.Remove(venda);
                _dbContext.SaveChanges();
                return new ObjectResult(venda)
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

        public async Task<VendaModel> BuscarVendaPorInformacoes(int idComprador, int idVenda, string status)
        {
            try
            {
                return _dbContext.Vendas.FirstOrDefault(x => x.id == idComprador && x.id == idVenda && x.venda_status == status);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
