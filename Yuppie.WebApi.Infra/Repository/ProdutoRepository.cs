using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuppie.WebApi.Infra.Context;
using Yuppie.WebApi.Infra.Models.Negociacao;
using Yuppie.WebApi.Infra.Models.Produto;

namespace Yuppie.WebApi.Infra.Repository
{
    public interface IProdutoRepository
    {
        public Task<ProdutoModel> BuscarProdutoPorId(int idProduto);
        public Task<List<ProdutoModel>> BuscarTodosProdutos();        
        public Task<ObjectResult> AdicionarProduto (ProdutoModel produto);
        public Task<ObjectResult> AtualizarProduto(ProdutoModel produto);
        public Task<ObjectResult> ExcluirProduto(int idProduto);
    }

    public class ProdutoRepository : IProdutoRepository
    {
        private readonly PostGreContext _dbContext;
        public ProdutoRepository(PostGreContext context) => _dbContext = context;     

        public async Task<ProdutoModel> BuscarProdutoPorId(int id)
        {
            try
            {
                return _dbContext.Produtos.Find(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<ProdutoModel>> BuscarTodosProdutos()
        {
            try
            {
                return _dbContext.Produtos.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }            
        }

        public async Task<ObjectResult> AdicionarProduto(ProdutoModel produto)
        {
            try
            {
                _dbContext.Produtos.Add(produto);
                _dbContext.SaveChanges();
                return new ObjectResult(produto)
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

        public async Task<ObjectResult> AtualizarProduto(ProdutoModel produto)
        {
            try
            {
                _dbContext.Produtos.Update(produto);
                _dbContext.SaveChanges();
                return new ObjectResult(produto)
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


        public async Task<ObjectResult> ExcluirProduto(int idProduto)
        {
            try
            {

                var produto = _dbContext.Produtos.Find(idProduto);
                _dbContext.Produtos.Remove(produto);
                _dbContext.SaveChanges();
                return new ObjectResult(produto)
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
