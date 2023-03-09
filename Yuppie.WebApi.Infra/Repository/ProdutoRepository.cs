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
        public Task<ProdutoModel> BuscarProdutoPorNome(string nomeProduto);
        public Task<List<ProdutoModel>> BuscarTodosProdutos();
        public Task<ObjectResult> AdicionarProduto(string categoria, string nome);
        public Task<ObjectResult> AtualizarProduto(string categoria, string nome);
        public Task<ObjectResult> ExcluirProduto(string categoria, string nome);
        public Task<ProdutoModel> BuscarProdutoPorId(int Id);
    }

    public class ProdutoRepository : IProdutoRepository
    {
        private readonly PostGreContext _dbContext;
        public ProdutoRepository(PostGreContext context) => _dbContext = context;

        public async Task<ProdutoModel> BuscarProdutoPorNome(string nomeProduto)
        {
            try
            {
                return _dbContext.Produtos.Where(x => x.Nome == nomeProduto).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<ProdutoModel> BuscarProdutoPorId(int Id)
        {
            try
            {
                return _dbContext.Produtos.Where(x => x.Id == Id).FirstOrDefault();
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

        public async Task<ObjectResult> AdicionarProduto(string categoria, string nome)
        {
            try
            {
                ProdutoModel produto = new ProdutoModel{
                    Categoria = categoria,
                    Nome = nome,
                    DataCriacao = DateTime.Now,
                    DataAtualizacao = DateTime.Now
                };

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

        public async Task<ObjectResult> AtualizarProduto(string categoria, string nome)
        {
            try
            {
                var produto = _dbContext.Produtos.Where(x => x.Nome == nome).FirstOrDefault();
                produto.DataAtualizacao = DateTime.Now;
                produto.Categoria = categoria;
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


        public async Task<ObjectResult> ExcluirProduto(string categoria, string nome)
        {
            try
            {
                var produto = _dbContext.Produtos.Where(x => x.Categoria == categoria && x.Nome == nome).FirstOrDefault();
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
