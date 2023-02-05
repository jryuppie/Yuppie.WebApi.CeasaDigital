using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Yuppie.WebApi.CeasaDigital.Domain.Interfaces;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Produto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Yuppie.WebApi.CeasaDigital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private IProdutoService _pgProdutoService;
        public ProdutoController(IProdutoService pgProdutoService )
        {
            _pgProdutoService = pgProdutoService;
        }
        [Route("GetProducts")]
        [HttpGet]
        public List<ProdutoModel> GetProducts()
        {
            var produtos = _pgProdutoService.BuscarTodosProdutos();
            return produtos;
        }

        [Route("GetProductsById")]
        [HttpGet]
        public List<ProdutoModel> GetProductsById(int Id)
        {
            return null;
        }

        [Route("GetProductsByType")]
        [HttpGet]
        public List<ProdutoModel> GetProductsByType(string Type)
        {
            return null;
        }

        [Route("GetProductsByRating")]
        [HttpGet]
        public List<ProdutoModel> GetProductsByRating(int Rating)
        {
            return null;
        }

    }
}
