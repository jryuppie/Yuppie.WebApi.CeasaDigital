using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yuppie.WebApi.CeasaDigital.Domain.Interfaces;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Formulario;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Produto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Yuppie.WebApi.CeasaDigital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private IProdutoService _pgProdutoService;
        public ProdutoController(IProdutoService pgProdutoService)
        {
            _pgProdutoService = pgProdutoService;
        }

        [Route("CadastrarProduto")]
        [HttpPost]
        public async Task<ObjectResult> CadastrarProduto([FromForm] ProdutoFormulario formModel)
        {
            return await _pgProdutoService.CadastrarProduto(formModel.Categoria, formModel.Nome);
        }

        [Route("AtualizarProduto")]
        [HttpPost]
        public async Task<ObjectResult> AtualizarProduto([FromForm] ProdutoFormulario formModel)
        {
            return await _pgProdutoService.AtualizarProduto(formModel.Categoria, formModel.Nome);
        }

        [Route("DeletarProduto")]
        [HttpPost]
        public async Task<ObjectResult> DeletarProduto([FromForm] ProdutoFormulario formModel)
        {
            return await _pgProdutoService.DeletarProduto(formModel.Categoria, formModel.Nome);
        }

        [Route("BuscarProdutos")]
        [HttpGet]
        public async Task<ObjectResult> BuscarProdutos()
        {
            return await _pgProdutoService.BuscarTodosProdutos();
        }

        [Route("BuscarProdutosPorNome")]
        [HttpGet]
        public async Task<ObjectResult> BuscarProdutosPorNome(string nomeProduto)
        {
            return await _pgProdutoService.BuscarProdutoPorNome(nomeProduto);
        }      
    }
}
