using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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
    [EnableCors]
    public class ProdutoController : ControllerBase
    {
        private IProdutoService _pgProdutoService;
        public ProdutoController(IProdutoService pgProdutoService)
        {
            _pgProdutoService = pgProdutoService;
        }

       //[Authorize]
        [HttpGet]
        public async Task<ObjectResult> BuscarProdutos()
        {
            return await _pgProdutoService.BuscarTodosProdutos();
        }

       //[Authorize]
        [Route("nome/{nome}")]
        [HttpGet]
        public async Task<ObjectResult> BuscarProdutosPorNome(string nomeProduto)
        {
            return await _pgProdutoService.BuscarProdutoPorNome(nomeProduto);
        }

       //[Authorize]
        [HttpPatch]
        public async Task<ObjectResult> AtualizarProduto([FromBody] ProdutoFormulario formModel)
        {
            return await _pgProdutoService.AtualizarProduto(formModel.Categoria, formModel.Nome);
        }

       //[Authorize]
        [HttpPost]
        public async Task<ObjectResult> CadastrarProduto([FromBody] ProdutoFormulario formModel)
        {
            return await _pgProdutoService.CadastrarProduto(formModel.Categoria, formModel.Nome);
        }

       //[Authorize]
        [HttpDelete]
        public async Task<ObjectResult> DeletarProduto([FromBody] ProdutoFormulario formModel)
        {
            return await _pgProdutoService.DeletarProduto(formModel.Categoria, formModel.Nome);
        }
    }
}
