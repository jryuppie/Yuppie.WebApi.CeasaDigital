using System;
using System.Collections.Generic;
using System.Text;

namespace Yuppie.WebApi.CeasaDigital.Domain.Models.Produto
{
    public class ProdutoModel
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string categoria { get; set; }
        public DateTime create_date { get; set; }
        public DateTime update_date { get; set; }
    }
}
