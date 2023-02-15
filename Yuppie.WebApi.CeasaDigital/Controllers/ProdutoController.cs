using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        [Route("CadastrarProduto")]
        [HttpGet]
        public async Task<ActionResult<ProdutoModel>> CadastrarProduto([FromForm] object formModel)
        {
            //verificar como esta sendo recebido o formulário
            var retorno = new ProdutoModel();
            return retorno;
        }

        [Route("AtualizarProduto")]
        [HttpPost]
        public async Task<ActionResult<ProdutoModel>> AtualizarProduto([FromForm] object formModel)
        {
            //verificar como esta sendo recebido o formulário
            var retorno = new ProdutoModel();
            return retorno;
        }

        [Route("DeletarProduto")]
        [HttpPost]
        public async Task<ActionResult<ProdutoModel>> DeletarProduto([FromForm] object formModel)
        {
            //verificar como esta sendo recebido o formulário
            var retorno = new ProdutoModel();
            return retorno;
        }

        [Route("BuscarProdutos")]
        [HttpGet]
        public List<ProdutoModel> GetProducts()
        {
            var produtos = _pgProdutoService.BuscarTodosProdutos();
            return produtos;
        }

        [Route("BuscarProdutosPorId")]
        [HttpGet]
        public List<ProdutoModel> GetProductsById(int Id)
        {
            return null;
        }

        [Route("BuscarProdutosPorTipo")]
        [HttpGet]
        public List<ProdutoModel> GetProductsByType(string Type)
        {
            return null;
        }

        [Route("BuscarProdutosPorRating")]
        [HttpGet]
        public List<ProdutoModel> GetProductsByRating(int Rating)
        {
            return null;
        }

        [Route("BuscarProdutosPorRating")]
        [HttpGet]
        public List<ProdutoModel> GetProductsByRating(int Rating)
        {
            return null;
        }

    }
}
