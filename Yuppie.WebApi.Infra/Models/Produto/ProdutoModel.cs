using System;
using System.Collections.Generic;
using System.Text;

namespace Yuppie.WebApi.Infra.Models.Produto
{
    public class ProdutoModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
    }
}
