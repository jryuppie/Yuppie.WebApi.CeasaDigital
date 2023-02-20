using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Produto;

namespace Yuppie.WebApi.CeasaDigital.Domain.Interfaces
{
    public interface IProdutoService
    {
        public Task<ObjectResult> BuscarTodosProdutos();

        public Task<ObjectResult> BuscarProdutoPorNome(string nome);

        public Task<ObjectResult> DeletarProduto(string categoria, string nome);

        public Task<ObjectResult> CadastrarProduto(string categoriaProduto, string nomeProduto);

        public Task<ObjectResult> AtualizarProduto(string categoriaProduto, string nomeProduto);
    }
}
