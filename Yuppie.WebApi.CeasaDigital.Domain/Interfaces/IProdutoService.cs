using System;
using System.Collections.Generic;
using System.Text;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Produto;

namespace Yuppie.WebApi.CeasaDigital.Domain.Interfaces
{
    public interface IProdutoService
    {
        public List<ProdutoModel> BuscarTodosProdutos();
    }
}
