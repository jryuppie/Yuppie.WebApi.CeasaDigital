using System;
using System.Collections.Generic;
using System.Text;

namespace Yuppie.WebApi.Infra.Models.Produto
{
    public class UnidadeMedidaModel
    {
        public int id { get; set; }
        public string nome { get; set; }        
        public DateTime create_date { get; set; }
        public DateTime update_date { get; set; }
    }
}

