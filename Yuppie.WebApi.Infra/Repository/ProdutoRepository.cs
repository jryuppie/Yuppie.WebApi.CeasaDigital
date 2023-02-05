using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yuppie.WebApi.Infra.Context;
using Yuppie.WebApi.Infra.Models.Produto;

namespace Yuppie.WebApi.Infra.Repository
{
    public interface IProdutoRepository
    {
        ProdutoModel GetProdutoById(int id);
        IEnumerable<ProdutoModel> GetAllProdutos();
        void AddProduto(ProdutoModel produto);
        void UpdateProduto(ProdutoModel produto);
        void DeleteProduto(int id);
    }

    public class ProdutoRepository : IProdutoRepository
    {
        private readonly PostGreContext _dbContext;
        public ProdutoRepository(PostGreContext context) => _dbContext = context;

        public ProdutoModel GetProdutoById(int id)
        {
            return _dbContext.Produtos.Find(id);
        }

        public IEnumerable<ProdutoModel> GetAllProdutos()
        {
            return _dbContext.Produtos.ToList();
        }

        public void AddProduto(ProdutoModel produto)
        {
            _dbContext.Produtos.Add(produto);
            _dbContext.SaveChanges();
        }

        public void UpdateProduto(ProdutoModel produto)
        {
            _dbContext.Produtos.Update(produto);
            _dbContext.SaveChanges();
        }

        public void DeleteProduto(int id)
        {
            var produto = _dbContext.Produtos.Find(id);
            _dbContext.Produtos.Remove(produto);
            _dbContext.SaveChanges();
        }
    }
}
