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
    [Authorize]
    public class ProdutoController : ControllerBase
    {
        private IProdutoService _pgProdutoService;
        public ProdutoController(IProdutoService pgProdutoService)
        {
            _pgProdutoService = pgProdutoService;
        }

        
        [HttpGet]
        public async Task<ObjectResult> BuscarProdutos()
        {
            return await _pgProdutoService.BuscarTodosProdutos();
        }

        
        [Route("nome/{nome}")]
        [HttpGet]
        public async Task<ObjectResult> BuscarProdutosPorNome(string nomeProduto)
        {
            return await _pgProdutoService.BuscarProdutoPorNome(nomeProduto);
        }

        
        [HttpPatch]
        public async Task<ObjectResult> AtualizarProduto([FromBody] ProdutoFormulario formModel)
        {
            return await _pgProdutoService.AtualizarProduto(formModel.Categoria, formModel.Nome);
        }

        
        [HttpPost]
        public async Task<ObjectResult> CadastrarProduto([FromBody] ProdutoFormulario formModel)
        {
            return await _pgProdutoService.CadastrarProduto(formModel.Categoria, formModel.Nome);
        }

        
        [HttpDelete]
        public async Task<ObjectResult> DeletarProduto([FromBody] ProdutoFormulario formModel)
        {
            return await _pgProdutoService.DeletarProduto(formModel.Categoria, formModel.Nome);
        }
    }
}
